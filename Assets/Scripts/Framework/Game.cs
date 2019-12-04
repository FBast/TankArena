using System;
using System.Collections.Generic;

namespace Framework {
    [Serializable]
    public class Game {

        public Match CurrentMatch;
        public List<Team> Teams = new List<Team>();
        public List<Match> Matches = new List<Match>();

        public void SetupTeamFight() {
            Matches.Add(new Match(Teams));
        }

        public void SetupTournament() {
            throw new NotImplementedException();
        }

        public Match NextMatch() {
            if (Matches.Count == 0)
                throw new Exception("No Matches found in Game");
            if (CurrentMatch == null) {
                CurrentMatch = Matches[0];
            }
            else {
                int currentMatchIndex = Matches.IndexOf(CurrentMatch);
                if (Matches.Count >= currentMatchIndex + 1) return null;
                CurrentMatch = Matches[currentMatchIndex + 1];
            }
            return CurrentMatch;
        }

    }
}
