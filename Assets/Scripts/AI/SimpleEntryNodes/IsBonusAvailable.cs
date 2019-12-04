using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class IsBonusAvailable : SimpleEntryNode {
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.BonusReference.Value.Count > 0 ? 1 : 0;
        }
    }
}