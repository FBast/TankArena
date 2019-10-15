using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class DataIsAgressor : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            return tankAiComponent.TankEntity.Aggressors.Contains(data) ? 1 : 0;
        }
        
    }
}