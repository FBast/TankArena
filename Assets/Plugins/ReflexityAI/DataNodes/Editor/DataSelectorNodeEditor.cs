using System;
using System.Linq;
using Plugins.ReflexityAI.Framework;
using UnityEditor;
using XNode;
using XNodeEditor;

namespace Plugins.ReflexityAI.DataNodes.Editor {
    [CustomNodeEditor(typeof(DataSelectorNode))]
    public class DataSelectorNodeEditor : NodeEditor {

        private DataSelectorNode _dataSelectorNode;

        public override void OnBodyGUI() {
            if (_dataSelectorNode == null) _dataSelectorNode = (DataSelectorNode) target;
            serializedObject.Update();
            if (_dataSelectorNode.SerializableInfos.Count > 0) {
                if (_dataSelectorNode.ChoiceIndex >= _dataSelectorNode.SerializableInfos.Count)
                    _dataSelectorNode.ChoiceIndex = 0;
                
                //TODO-fred Add parameters backing field
//                SerializedProperty property = serializedObject.FindProperty(nameof(_dataSelectorNode.Parameters));
//                for (int i = 0; i < property.arraySize; i++) {
//                    NodePort port = _dataSelectorNode.DynamicInputs.ElementAt(i);
//                    if (port.IsConnected) {
//                        NodeEditorGUILayout.PortField(port);
//                    } 
//                    else {
//                        EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), new GUIContent(port.fieldName));
//                        NodeEditorGUILayout.AddPortField(port);
//                    }
//                }

                foreach (NodePort dynamicInput in _dataSelectorNode.DynamicInputs) {
                    NodeEditorGUILayout.PortField(dynamicInput);
                }
                
                string[] choices = _dataSelectorNode.SerializableInfos.Select(info => info.Name).ToArray();
                //BUG-fred ArgumentException: Getting control 2's position in a group with only 2 controls when doing mouseUp
                int choiceIndex = EditorGUILayout.Popup(_dataSelectorNode.ChoiceIndex, choices);
                if (choiceIndex != _dataSelectorNode.ChoiceIndex) {
                    UpdateChoice(choiceIndex);
                }
                NodePort dataPort = _dataSelectorNode.GetPort(nameof(_dataSelectorNode.Data));
                NodeEditorGUILayout.AddPortField(dataPort);
                NodePort nodePort = _dataSelectorNode.GetPort(nameof(_dataSelectorNode.Output));
                nodePort.ValueType = _dataSelectorNode.SelectedSerializableInfo.Type;
                NodeEditorGUILayout.AddPortField(nodePort);
            }
            else {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_dataSelectorNode.Data)));
            }
            serializedObject.ApplyModifiedProperties();
        }
        
        public void UpdateChoice(int choiceIndex) {
            _dataSelectorNode.ChoiceIndex = choiceIndex;
            _dataSelectorNode.SelectedSerializableInfo = _dataSelectorNode.SerializableInfos
                .ElementAt(_dataSelectorNode.ChoiceIndex);
            _dataSelectorNode.ClearDynamicPorts();
            _dataSelectorNode.Parameters.Clear();
            foreach (Parameter parameter in _dataSelectorNode.SelectedSerializableInfo.Parameters) {
                Type parameterType = Type.GetType(parameter.TypeName);
                _dataSelectorNode.AddDynamicInput(parameterType, Node.ConnectionType.Override, 
                    Node.TypeConstraint.InheritedInverse, parameter.Name);
            }
        }
        
    }
}