using System;
using Data.SOReferences;

namespace SOReferences.FloatReference {
    [Serializable]
    public class FloatReference : Reference<float, FloatVariable> {
        public FloatReference(float Value) : base(Value) { }
        public FloatReference() { }
    }
}
