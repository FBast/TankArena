using System.Collections.Generic;
using NodeUtilityAi;
using UnityEngine;

namespace Entities {
    [CreateAssetMenu(fileName = "NewTankSetting", menuName = "TankSetting")]
    public class TankSetting : ScriptableObject {

        public string PlayerName;
        public string TankName;
        public Color TurretColor;
        public Color HullColor;
        public Color TracksColor;
        public List<AbstractAIBrain> Brains;

    }
}