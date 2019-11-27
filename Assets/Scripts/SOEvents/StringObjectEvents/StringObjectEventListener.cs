using System;

namespace SOEvents.StringObjectEvents {
    public class StringObjectEventListener : BaseGameEventListener<Tuple<string, object>, StringObjectEvent, UnityStringObjectEvent> {}
}