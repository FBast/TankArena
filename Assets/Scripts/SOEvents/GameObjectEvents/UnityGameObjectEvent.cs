using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOEvents.GameObjectEvents {
    [Serializable] public class UnityGameObjectEvent : UnityEvent<GameObject> {}
}