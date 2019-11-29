using TMPro;
using UnityEngine;

namespace UI {
    public class TeamTankUI : MonoBehaviour {

        [Header("Prefabs")]
        public GameObject TankImage;

        [Header("Internal References")] 
        public TextMeshProUGUI TeamNameText;
        public Transform TankImageContent;
    
        public void AddTankImage() {
            Instantiate(TankImage, TankImageContent);
        }
    
    }
}
