using System;
using System.Collections.Generic;
using System.Linq;
using NodeUtilityAi.Framework;
using NodeUtilityAi.Nodes;
using Plugins.NodeUtilityAi.Framework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace NodeUtilityAi {
    public class AbstractAIComponent : MonoBehaviour {

        [Header("Debug")] 
        public GameObject DebugCanvasPrefab;
        public bool DebugMode;

        [Header("Brains")]
        public bool AlwaysPickBestChoice;
        public List<AbstractAIBrain> UtilityAiBrains;
        
        private readonly Dictionary<string, Object> _memory = new Dictionary<string, Object>();
        private float _lastProbabilityResult;
        private readonly Dictionary<AbstractAIBrain, List<AIOption>> _options = new Dictionary<AbstractAIBrain, List<AIOption>>();
        private static AIDebug _aiDebug;

        private void Update() {
#if UNITY_EDITOR
            if (_aiDebug == null) _aiDebug = Instantiate(DebugCanvasPrefab).GetComponent<AIDebug>();
            if (Selection.Contains(gameObject) && Selection.transforms.Length == 1) {
                DisplayDebug(_options);
            }
#endif
        }

        private AIOption ChooseOption(AbstractAIBrain utilityAiBrain) {
            if (utilityAiBrain == null) return null;
            // Setup Contexts
            utilityAiBrain.GetNodes<EntryNode>().ForEach(node => node.SetContext(this));
            utilityAiBrain.GetNodes<ActionNode>().ForEach(node => node.SetContext(this));
            // Add the brain to the option dictionary
            if (_options.ContainsKey(utilityAiBrain)) {
                _options[utilityAiBrain].Clear();
            }
            else {
                _options.Add(utilityAiBrain, new List<AIOption>());
            }
            utilityAiBrain.GetNodes<OptionNode>().ForEach(node => _options[utilityAiBrain]
                .AddRange(node.GetOptions()));
            // Remove ImpossibleDecisionValue Ranks
            _options[utilityAiBrain].RemoveAll(option => option.Rank <= 0f);
            if (_options.Count == 0)
                return null;
            // Get max Rank
            int maxRank = _options[utilityAiBrain].Max(option => option.Rank);
            for (int i = maxRank; i > 0; i--) {
                List<AIOption> options = _options[utilityAiBrain].FindAll(utility => utility.Rank == i);
                if (options.Count == 0 || options.Sum(utility => utility.Utility) <= 0) continue;
                // Calculating Weight
                options.ForEach(dualUtility => dualUtility.Weight = dualUtility.Utility / _options[utilityAiBrain]
                                                                        .Sum(utility => utility.Utility));
                options = options.OrderByDescending(option => option.Weight).ToList();
                // Returning best option for no random
                if (AlwaysPickBestChoice) {
                    return options.FirstOrDefault();
                }
                // Rolling probability on weighted random
                _lastProbabilityResult = Random.Range(0f, 1f);
                float weightSum = 0f;
                foreach (AIOption dualUtility in options) {
                    weightSum += dualUtility.Weight;
                    if (weightSum >= _lastProbabilityResult)
                        return dualUtility;
                }
            }
            return null;
        }

        public void DisplayDebug(Dictionary<AbstractAIBrain, List<AIOption>> options) {
            // Return if selection does not contain this or selection is multiple
            if (!Selection.Contains(gameObject) || Selection.transforms.Length != 1) return;
            if (!DebugMode) return;
            _aiDebug.ClearChoices();
            foreach (KeyValuePair<AbstractAIBrain,List<AIOption>> valuePair in options) {
                foreach (AIOption option in valuePair.Value) {
                    _aiDebug.AddChoice(valuePair.Key, option);
                }
            }
        }

        public void ThinkAndAct() {
            // Create canvas if null
            foreach (AbstractAIBrain utilityAiBrain in UtilityAiBrains) {
                AIOption option = ChooseOption(utilityAiBrain);
                option?.ExecuteActions(this);
            }
        }
        
        public void SaveToMemory(string dataTag, Object data) {
            if (LoadFromMemory(dataTag) != null)
                throw new Exception("Impossible to save " + dataTag + ", consider using a " + typeof(MemoryCheckNode)
                    + " before using " + typeof(MemoryAccessNode));
            _memory.Add(dataTag, data);
        }

        public TaggedData LoadFromMemory(string dataTag) {
            Object dataToReturn;
            if (!_memory.TryGetValue(dataTag, out dataToReturn)) return null;
            TaggedData taggedData = new TaggedData {DataTag = dataTag, Data = dataToReturn};
            return taggedData;
        }
        
        public bool ClearFromMemory(string dataTag) {
            return _memory.Remove(dataTag);
        }
        
    }
}