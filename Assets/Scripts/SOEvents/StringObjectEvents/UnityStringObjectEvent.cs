using System;
using UnityEngine.Events;

namespace SOEvents.StringObjectEvents {
    [Serializable] public class UnityStringObjectEvent : UnityEvent<Tuple<string, object>> {}
}