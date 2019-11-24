using Entities;
using NodeUtilityAi;
using UnityEngine;
using Utils;

namespace AI {
    public class TankAIComponent : AbstractAIComponent 
    {

        // Your custom references here
        [HideInInspector] public TankEntity TankEntity;
        // End of custom references

        private void Awake() {
            TankEntity = GetComponent<TankEntity>();
            TimeBetweenRefresh = PlayerPrefs.GetInt(Properties.PlayerPrefs.SecondsBetweenRefresh, Properties.PlayerPrefsDefault.SecondsBetweenRefresh);
            AlwaysPickBestChoice = PlayerPrefsUtils.GetBool(Properties.PlayerPrefs.AlwaysPickBestChoice, Properties.PlayerPrefsDefault.AlwaysPickBestChoice);
        }

    }
}