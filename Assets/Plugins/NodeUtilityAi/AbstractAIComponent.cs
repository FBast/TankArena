using System;
using System.Collections.Generic;
using System.Linq;
using NodeUtilityAi.Framework;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace NodeUtilityAi {
    public class AbstractAIComponent : MonoBehaviour {

        public bool DebugMode;
        public bool NoRandom;
        public List<AbstractAIBrain> UtilityAiBrains;
        public Dictionary<string, Object> Memory = new Dictionary<string, Object>();
        
        private float _lastProbabilityResult;
        private List<AIOption> _options;
        
        private AIOption ChooseOption(AbstractAIBrain utilityAiBrain) {
            if (utilityAiBrain == null) return null;
            utilityAiBrain.GetNodes<EntryNode>().ForEach(node => node.SetContext(this));
            utilityAiBrain.GetNodes<ActionNode>().ForEach(node => node.SetContext(this));
            // Fill the Ranked Options
            _options = new List<AIOption>();
            utilityAiBrain.GetNodes<OptionNode>().ForEach(node => _options.AddRange(node.GetOptions()));
            // Remove ImpossibleDecisionValue Ranks
            _options.RemoveAll(option => option.Rank <= 0f);
            if (_options.Count == 0)
                return null;
            // Get max Rank
            int maxRank = _options.Max(option => option.Rank);
            for (int i = maxRank; i > 0; i--) {
                List<AIOption> options = _options.FindAll(utility => utility.Rank == i);
                if (options.Count == 0 || options.Sum(utility => utility.Utility) <= 0) continue;
                // Calculating Weight
                options.ForEach(dualUtility => dualUtility.Weight = dualUtility.Utility / _options.Sum(utility => utility.Utility));
                // Displaying debug
                // Returning best option for no random
                if (NoRandom) {
                    return options.OrderByDescending(option => option.Weight).FirstOrDefault();
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

        public void ThinkAndAct() {
            foreach (AbstractAIBrain utilityAiBrain in UtilityAiBrains) {
                AIOption option = ChooseOption(utilityAiBrain);
                option?.ExecuteActions(this);
                if (DebugMode) Debug.Log(name + " for " + utilityAiBrain.name + " choose " + option);
            }
        }
        
        public void SaveToMemory(string dataTag, Object data) {
            if (LoadFromMemory(dataTag) != null)
                throw new Exception("Impossible to save " + dataTag + ", consider using a " + typeof(MemoryCheckNode)
                    + " before using " + typeof(MemoryAccessNode));
            Memory.Add(dataTag, data);
        }

        public TaggedData LoadFromMemory(string dataTag) {
            Object dataToReturn;
            if (!Memory.TryGetValue(dataTag, out dataToReturn)) return null;
            TaggedData taggedData = new TaggedData {DataTag = dataTag, Data = dataToReturn};
            return taggedData;
        }
        
        public bool ClearFromMemory(string dataTag) {
            return Memory.Remove(dataTag);
        }
        
    }

}