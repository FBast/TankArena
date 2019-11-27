using System.Collections.Generic;
using System.Threading.Tasks;
using NodeUtilityAi;
using UnityEngine;
using Utils;

namespace Data {
    [CreateAssetMenu(fileName = "NewTankSetting", menuName = "TankSetting")]
    public class TankSetting : ScriptableObject {

        public string PlayerName;
        public string TankName;
        public Color TurretColor;
        public Color HullColor;
        public Color TracksColor;
        public List<AbstractAIBrain> Brains = new List<AbstractAIBrain>();

        public async Task Save() {
            await DataHandler.Save(this);
        }
        
    }
}