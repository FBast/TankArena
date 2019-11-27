using System;
using Data.SOReferences;
using UnityEngine;

namespace SOReferences.RectTransformReference {
    [Serializable]
    public class RectTransformReference : Reference<RectTransform, RectTransformVariable> {
        public RectTransformReference(RectTransform Value) : base(Value) { }
        public RectTransformReference() { }
    }
}