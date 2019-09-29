using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Framework;
using NodeUtilityAi.Nodes;

namespace AI.SimpleActionNodes {
    public class FireNode : SimpleActionNode {

        public override void Execute(AbstractAIComponent context, AIData aiData) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            tankAiComponent.TankEntity.Fire();
        }
        
    }

    public class TargetNode : SimpleActionNode {

        public override void Execute(AbstractAIComponent context, AIData aiData) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            tankAiComponent.TankEntity.Target = aiData.GetData<TankEntity>();
        }
        
    }
}