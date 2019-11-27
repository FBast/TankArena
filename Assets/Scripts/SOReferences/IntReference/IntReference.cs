using System;
using Data.SOReferences;

namespace SOReferences.IntReference {
    [Serializable]
    public class IntReference : Reference<int, IntVariable> {
        public IntReference(int Value) : base(Value) { }
        public IntReference() { }
    }
}