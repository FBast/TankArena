using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class DataDistance : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject target = GetData<GameObject>();
            return (int) Vector3.Distance(tankAiComponent.gameObject.transform.position, target.transform.position);
        }
    
    }
}