using Entities;
using NodeUtilityAi;
using UnityEngine;

namespace AI {
    public class TankAIComponent : AbstractAIComponent 
    {

        // Your custom references here
        [HideInInspector] public TankEntity TankEntity;
        // End of custom references

        private void Awake() {
            TankEntity = GetComponent<TankEntity>();
        }

    }
}