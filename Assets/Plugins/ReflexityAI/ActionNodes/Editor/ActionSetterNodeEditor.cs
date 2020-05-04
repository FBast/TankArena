using System.Linq;
using UnityEditor;
using XNode;
using XNodeEditor;

namespace Plugins.ReflexityAI.ActionNodes.Editor {
    [CustomNodeEditor(typeof(ActionSetterNode))]
    public class ActionSetterNodeEditor : NodeEditor {

        private ActionSetterNode _actionSetterNode;

        public override void OnBodyGUI() {
            if (_actionSetterNode == null) _actionSetterNode = (ActionSetterNode) target;
            if (_actionSetterNode.SerializableInfos.Count > 0) {
                NodeEditorGUILayout.PortField(_actionSetterNode.GetPort(nameof(_actionSetterNode.Value)));
                string[] choices = _actionSetterNode.SerializableInfos.Select(info => info.Name).ToArray();
                //BUG-fred ArgumentException: Getting control 2's position in a group with only 2 controls when doing mouseUp
                _actionSetterNode.ChoiceIndex = EditorGUILayout.Popup(_actionSetterNode.ChoiceIndex, choices);;
                _actionSetterNode.SelectedSerializableInfo = _actionSetterNode.SerializableInfos.ElementAt(_actionSetterNode.ChoiceIndex);
                NodePort valuePort = _actionSetterNode.GetPort(nameof(_actionSetterNode.Value));
                valuePort.ValueType = _actionSetterNode.SelectedSerializableInfo.Type;
                NodeEditorGUILayout.AddPortField(_actionSetterNode.GetPort(nameof(_actionSetterNode.Data)));
                NodeEditorGUILayout.AddPortField(_actionSetterNode.GetPort(nameof(_actionSetterNode.LinkedOption)));
            } else {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(_actionSetterNode.Data)));
            }
        }
        
    }
}