using System;
using Data.SOReferences;
using UnityEngine;

namespace SOReferences.GameObjectReference {
    [Serializable]
    public class GameObjectReference : Reference<GameObject, GameObjectVariable> {}
}