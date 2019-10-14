using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.DataEntryNodes {
    public class DataInRayIsTarget : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.TankInRay() == tankAiComponent.TankEntity.Target ? 1 : 0;
        }
        
    }
}