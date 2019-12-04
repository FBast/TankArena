using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Framework {
    public class Team {

        public Color Color = Color.black;
        public List<TankSetting> TankSettings = new List<TankSetting>();
        
        public string TeamName => TankSettings
            .Select(setting => setting.PlayerName)
            .Distinct()
            .Aggregate((i, j) => i + " & " + j);
        
    }
}