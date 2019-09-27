using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.EntryNodes {
    public class IsShellLoadedNode : SimpleEntryNode 
    {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return tankAiComponent.TankEntity.IsShellLoaded ? 1 : 0;
        }
    }
}