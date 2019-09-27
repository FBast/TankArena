using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.EntryNodes {
    public class IsTankInRayNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.IsTankInRay() ? 1 : 0;
        }
        
    }
}