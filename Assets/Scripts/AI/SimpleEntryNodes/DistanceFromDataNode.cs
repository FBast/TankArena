using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromDataNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject target = GetData<GameObject>();
            return (int) Vector3.Distance(tankAiComponent.gameObject.transform.position, target.transform.position);
        }
    
    }
}