using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class AliveTank : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            return GameManager.Instance.TankEntities.Count;
        }
        
    }
}