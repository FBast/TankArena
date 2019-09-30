using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Framework;
using UnityEngine;

namespace AI {
    public class TankAIComponent : AbstractAIComponent 
    {

        // Your custom references here
        [HideInInspector] public TankEntity TankEntity;
        // End of custom references

        private void Start() {
            TankEntity = GetComponent<TankEntity>();
            InvokeRepeating(nameof(ThinkAndAct), 0, 0.1f);
        }

    }
}