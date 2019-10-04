using UnityEngine;

namespace Entities {
    [CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject {

        public string PlayerName;
        public string TankName;
        public Color TurretColor;
        public Color HullColor;
        public Color TracksColor;

    }
}