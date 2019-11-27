using System;
using Data.SOReferences;
using UnityEngine;

namespace SOReferences.Vector3Reference {
    [Serializable]
    public class Vector3Reference : Reference<Vector3, Vector3Variable> {
        public Vector3Reference(Vector3 Value) : base(Value) { }
        public Vector3Reference() { }
    }
}