using NodeUtilityAi;
using NodeUtilityAi.Framework;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleActionNodes {
    public class SetDestinationNode : SimpleActionNode {

        public override void Execute(AbstractAIComponent context, AIData aiData) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            tankAiComponent.TankEntity.Destination = aiData.GetData<GameObject>();
        }
        
    }
}