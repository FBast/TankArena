using System;
using Data.SOReferences;
using UnityEngine;

namespace SOReferences.Texture2DReference {
    [Serializable]
    public class Texture2DReference : Reference<Texture2D, Texture2DVariable> {
        public Texture2DReference(Texture2D Value) : base(Value) { }
        public Texture2DReference() { }
    }
}