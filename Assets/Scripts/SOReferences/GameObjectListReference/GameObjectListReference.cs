using System;
using System.Collections.Generic;
using UnityEngine;

namespace SOReferences.GameObjectListReference {
    [Serializable]
    public class GameObjectListReference : Reference<List<GameObject>, GameObjectListVariable> {}
}