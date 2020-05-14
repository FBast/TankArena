using System.Collections.Generic;
using System.Linq;
using Plugins.ReflexityAI.Framework;
using UnityEngine;
using XNode;
using Object = UnityEngine.Object;

namespace Plugins.ReflexityAI.DataNodes {
    public class DataSelectorNode : DataNode, ICacheable {
        
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)] public Object Data;
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)] public Object Output;

        public SerializableInfo SelectedSerializableInfo;
        [HideInInspector] public List<SerializableInfo> SerializableInfos = new List<SerializableInfo>();
        [HideInInspector] public int ChoiceIndex;

        public List<object> Parameters = new List<object>();
        public ObjectDictionary ObjectDictionary = new ObjectDictionary();
        
        public void UpdateData() {
            SerializableInfos.Clear();
            BoxedData boxedData = GetInputValue<BoxedData>(nameof(Data));
            SerializableInfos.AddRange(boxedData.Type
                .GetFields(SerializableInfo.DefaultBindingFlags)
                .Select(info => new SerializableInfo(info, boxedData.FromIteration)));
            SerializableInfos.AddRange(boxedData.Type
                .GetProperties(SerializableInfo.DefaultBindingFlags)
                .Select(info => new SerializableInfo(info, boxedData.FromIteration)));
            SerializableInfos.AddRange(boxedData.Type
                .GetMethods(SerializableInfo.DefaultBindingFlags)
                .Select(info => new SerializableInfo(info, boxedData.FromIteration)));
        }
        
        public override void OnCreateConnection(NodePort from, NodePort to) {
            if (to.fieldName == nameof(Data) && to.node == this) {
                UpdateData();
            }
        }
        
        public override void OnRemoveConnection(NodePort port) {
            if (port.fieldName == nameof(Data)) {
                SerializableInfos.Clear();
            }
        }
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(Output)) {
                List<object> parameters = new List<object>();
                if (SelectedSerializableInfo.Parameters.Count > 0) {
                    foreach (Parameter parameter in SelectedSerializableInfo.Parameters) {
                        NodePort inputPort = GetInputPort(parameter.Name);
                        object inputValue = inputPort.GetInputValue();
                        if (inputValue is BoxedData data) inputValue = data.Value;
                        parameters.Add(inputValue);
//                        parameters.Add(inputPort.IsConnected ? inputPort.GetInputValue() : ObjectDictionary[parameter.Name]);
                    }
                }
                BoxedData boxedData = GetInputValue<BoxedData>(nameof(Data));
                return Application.isPlaying
                    ? SelectedSerializableInfo.GetRuntimeValue(boxedData.Value, parameters.ToArray())
                    : SelectedSerializableInfo.GetEditorValue();
            }
            return null;
        }

        public void ClearCache() {
            SelectedSerializableInfo.ClearCache();
        }

        public void ClearShortCache() {
            if (SelectedSerializableInfo.IsShortCache) SelectedSerializableInfo.ClearCache();
        }
    }
}