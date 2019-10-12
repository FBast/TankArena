using System.Linq;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class AliveTankNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            return GameManager.Instance.TankEntities.Count;
        }
        
    }
}