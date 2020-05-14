using Plugins.ReflexityAI.Framework;
using UnityEngine;
using XNode;

namespace Plugins.ReflexityAI.MiddleNodes {
    public class LengthNode : MiddleNode {
        
        [Input(ShowBackingValue.Never)] public Object[] ValueIn;
        [Output] public int ValueOut;
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(ValueOut)) {
                object o = GetInputValue<object>(nameof(ValueIn));
                if (o is BoxedData data) {
                    o = data.Value;
                }
                return ((object[]) o).Length;
            }
            return null;
        }
        
    }
}