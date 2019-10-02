using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DataIsTarget : SimpleEntryNode {
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return GetData<GameObject>() == tankAiComponent.TankEntity.Target ? 1 : 0;
        }
        
    }
}