using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.EntryNodes {
    public class StartingHpCountNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.StartingHP;
        }
        
    }
}