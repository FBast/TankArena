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
            // Starting with FFA
            Matches.Add(new Match(Teams));
            // Round Robin
            List<Match> RRMatches = new List<Match>();
            foreach (Team FirstTeam in Teams) {
                foreach (Team SecondTeam in Teams) {
                    if (FirstTeam == SecondTeam) continue;
                    if (RRMatches.Exists(match => match.Teams.Contains(FirstTeam) && match.Teams.Contains(SecondTeam))) continue;
                    RRMatches.Add(new Match(new List<Team> {FirstTeam, SecondTeam}));
                }
            }
            Matches.AddRange(RRMatches);
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
