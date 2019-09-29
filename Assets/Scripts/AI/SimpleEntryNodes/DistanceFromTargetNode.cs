using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromTargetNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            TankEntity tankEntityTarget = GetData<TankEntity>();
            return (int) Vector3.Distance(tankAiComponent.gameObject.transform.position, tankEntityTarget.transform.position);
        }
    
    }
}