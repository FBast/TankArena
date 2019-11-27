using System;
using Data.SOReferences;
using Framework;

namespace SOReferences.MatchReference {
    [Serializable]
    public class MatchReference : Reference<Match, MatchVariable> {
        public MatchReference(Match Value) : base(Value) { }
        public MatchReference() { }
    }
}