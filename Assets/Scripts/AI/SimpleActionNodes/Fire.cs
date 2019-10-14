using NodeUtilityAi;
using NodeUtilityAi.Framework;
using NodeUtilityAi.Nodes;

namespace AI.SimpleActionNodes {
    public class Fire : SimpleActionNode {

        public override void Execute(AbstractAIComponent context, AIData aiData) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            tankAiComponent.TankEntity.Fire();
        }
        
    }

}