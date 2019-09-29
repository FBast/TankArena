using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class AliveTankNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            return FindObjectsOfType<TankEntity>().Length;
        }
        
    }
}