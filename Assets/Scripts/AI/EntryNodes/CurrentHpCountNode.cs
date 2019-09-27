using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.EntryNodes {
    public class CurrentHpCountNode : SimpleEntryNode
    {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.CurrentHP;
        }
        
    }
}
