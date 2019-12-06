using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class TargetIsInRay : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.TankInRay() == tankAiComponent.TankEntity.Target ? 1 : 0;
        }
        
    }
}