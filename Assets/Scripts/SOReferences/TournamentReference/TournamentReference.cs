using System;
using Data.SOReferences;
using Framework;

namespace SOReferences.TournamentReference {
    [Serializable]
    public class TournamentReference : Reference<Tournament, TournamentVariable> {
        public TournamentReference(Tournament Value) : base(Value) { }
        public TournamentReference() { }
    }
}
