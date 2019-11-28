using System.Collections.Generic;

namespace Framework {
    public class Match {
        
        public List<Team> Teams = new List<Team>();
        public Dictionary<Team, MatchStat> MatchStats = new Dictionary<Team, MatchStat>();

        public void AddTeam(Team team) {
            Teams.Add(team);
            MatchStats.Add(team, new MatchStat());
        }

        public void ClearStats() {
            Dictionary<Team, MatchStat> matchStats = new Dictionary<Team, MatchStat>();
            foreach (KeyValuePair<Team,MatchStat> matchStat in MatchStats) {
                matchStats.Add(matchStat.Key, new MatchStat());
            }
            MatchStats = matchStats;
        }
        
    }
}
