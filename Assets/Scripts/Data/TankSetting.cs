using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using Plugins.ReflexityAI.Framework;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(fileName = "NewTankSetting", menuName = "TankSetting")]
    public class TankSetting : ScriptableObject {

        public string PlayerName;
        public string TankName;
        public Color TurretColor;
        public Color HullColor;
        public Color TracksColor;
        public List<AIBrainGraph> Brains = new List<AIBrainGraph>();
        
    }
}