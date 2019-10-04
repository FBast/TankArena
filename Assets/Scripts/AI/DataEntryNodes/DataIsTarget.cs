using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class DataIsTarget : DataEntryNode {
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return GetData<GameObject>() == tankAiComponent.TankEntity.Target ? 1 : 0;
        }
        
    }
}