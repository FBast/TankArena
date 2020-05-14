using Plugins.ReflexityAI.Framework;
using UnityEngine;
using XNode;

namespace Plugins.ReflexityAI.MiddleNodes {
    public class DistanceNode : MiddleNode {
        
        [Input(ShowBackingValue.Never, ConnectionType.Override)] public Vector3 FirstValueIn;
        [Input(ShowBackingValue.Never)] public Vector3 SecondValueIn;
        [Output] public float ValueOut;
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(ValueOut)) {
                FirstValueIn = (Vector3) GetInputValue<BoxedData>(nameof(FirstValueIn)).Value;
                SecondValueIn = (Vector3) GetInputValue<BoxedData>(nameof(SecondValueIn)).Value;
                float distance = Vector3.Distance(FirstValueIn, SecondValueIn);
                return distance;
            }
            return null;
        }
        
    }
}