using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class DataDistanceFromTarget : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject target = GetData<GameObject>();
            if (tankAiComponent.TankEntity.Target == null) return 0;
            return (int) Vector3.Distance(tankAiComponent.TankEntity.Target.transform.position, target.transform.position);
        }
    
    }
}