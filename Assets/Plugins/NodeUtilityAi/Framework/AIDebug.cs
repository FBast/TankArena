using System;
using System.Collections.Generic;
using NodeUtilityAi;
using NodeUtilityAi.Framework;
using UnityEngine;

namespace Plugins.NodeUtilityAi.Framework {
    public class AIDebug : MonoBehaviour {

        public GameObject BrainsPanel;
        public GameObject ChoicesPanelPrefab;
        public GameObject ChoiceBarPrefab;

        private readonly Dictionary<AbstractAIBrain, GameObject> _choicesPanels = 
            new Dictionary<AbstractAIBrain, GameObject>();
        private readonly Dictionary<AbstractAIBrain, Stack<AIDebugChoice>> _enabledAiDebugChoices = 
            new Dictionary<AbstractAIBrain, Stack<AIDebugChoice>>();
        private readonly Dictionary<AbstractAIBrain, Stack<AIDebugChoice>> _disabledAiDebugChoices = 
            new Dictionary<AbstractAIBrain, Stack<AIDebugChoice>>();

        public void AddChoice(AbstractAIBrain abstractAiBrain, AIOption aiOption) {
            // Search disabled Stack for abstractAiBrain
            Stack<AIDebugChoice> disabledStackChoices;
            if (_disabledAiDebugChoices.ContainsKey(abstractAiBrain)) {
                disabledStackChoices = _disabledAiDebugChoices[abstractAiBrain];
            }
            else {
                disabledStackChoices = new Stack<AIDebugChoice>();
                _disabledAiDebugChoices.Add(abstractAiBrain, disabledStackChoices);
            }
            // Search enabled Stack for abstractAiBrain
            Stack<AIDebugChoice> enabledStackChoices;
            if (_enabledAiDebugChoices.ContainsKey(abstractAiBrain)) {
                enabledStackChoices = _enabledAiDebugChoices[abstractAiBrain];
            }
            else {
                enabledStackChoices = new Stack<AIDebugChoice>();
                _enabledAiDebugChoices.Add(abstractAiBrain, enabledStackChoices);
            }
            // Use a disabled choice
            AIDebugChoice aiDebugChoice;
            if (disabledStackChoices.Count > 0) {
                aiDebugChoice = disabledStackChoices.Pop();
                aiDebugChoice.gameObject.SetActive(true);
            }
            else {
                // Handle choicePanel instantiation
                GameObject choicesPanel;
                if (_choicesPanels.ContainsKey(abstractAiBrain)) {
                    choicesPanel = _choicesPanels[abstractAiBrain];
                }
                else {
                    choicesPanel = Instantiate(ChoicesPanelPrefab, BrainsPanel.transform);
                    _choicesPanels.Add(abstractAiBrain, choicesPanel);
                }
                // Handle choice instantiation
                GameObject choicePanel = Instantiate(ChoiceBarPrefab, choicesPanel.transform);
                aiDebugChoice = choicePanel.GetComponent<AIDebugChoice>();
                if (aiDebugChoice == null)
                    throw new Exception("Your choice bar prefab does not contain the component AIDebug, " +
                                        "it is mandatory to use the debug feature");
            }
            aiDebugChoice.ChoiceName.text = aiOption.Description;
            aiDebugChoice.ChoiceSlider.value = aiOption.Utility;
            enabledStackChoices.Push(aiDebugChoice);
        }

        public void ClearChoices() {
            foreach (KeyValuePair<AbstractAIBrain,Stack<AIDebugChoice>> valuePair in _enabledAiDebugChoices) {
                while (valuePair.Value.Count > 0) {
                    AIDebugChoice aiDebugChoice = valuePair.Value.Pop();
                    aiDebugChoice.gameObject.SetActive(false);
                    Stack<AIDebugChoice> disabledStackChoices;
                    if (_disabledAiDebugChoices.ContainsKey(valuePair.Key)) {
                        disabledStackChoices = _disabledAiDebugChoices[valuePair.Key];
                    }
                    else {
                        disabledStackChoices = new Stack<AIDebugChoice>();
                        _disabledAiDebugChoices.Add(valuePair.Key, disabledStackChoices);
                    }
                    disabledStackChoices.Push(aiDebugChoice);
                }
            }
        }
        
    }
}