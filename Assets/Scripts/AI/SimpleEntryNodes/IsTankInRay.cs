using Entities;
using Framework;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class IsTankInRay : SimpleEntryNode {

        public FactionType FactionType = FactionType.All;
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            if (FactionType == FactionType.All)
                return tankAiComponent.TankEntity.TankInRay() ? 1 : 0;
            GameObject tankInRay = tankAiComponent.TankEntity.TankInRay();
            if (tankInRay != null && tankInRay.GetComponent<TankEntity>().GetFaction(tankAiComponent.TankEntity) == FactionType)
                return 1;
            return 0;
        }
        
    }
}