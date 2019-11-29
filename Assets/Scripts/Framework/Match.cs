using System.Collections.Generic;
using System.Linq;

namespace Framework {
    public class Match {

        public List<Team> Teams;
        public Dictionary<Team, Stats> TeamStats = new Dictionary<Team, Stats>();
        public Team Winner;

        public IEnumerable<Team> TeamInMatch => TeamStats
            .Where(pair => !pair.Value.IsDefeated)
            .Select(pair => pair.Key);
        
        public Match(List<Team> teams) {
            Teams = teams;
            foreach (Team team in teams) {
                Stats stats = new Stats {TankLeft = team.TankSettings.Count};
                TeamStats.Add(team, stats);
            }
        }
        
    }
}
