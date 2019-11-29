using System.Collections.Generic;

namespace Framework {
    public class Match {

        public List<Team> Teams;
        public Dictionary<Team, Stats> TeamStats = new Dictionary<Team, Stats>();

        public Match(List<Team> teams) {
            Teams = teams;
            foreach (Team team in teams) {
                Stats stats = new Stats {TankLeft = team.TankSettings.Count};
                TeamStats.Add(team, stats);
            }
        }
        
    }
}
