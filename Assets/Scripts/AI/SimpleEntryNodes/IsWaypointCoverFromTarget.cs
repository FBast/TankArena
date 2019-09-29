using System.Collections.Generic;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class IsWaypointCoverFromTarget : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject waypoint = GetData<GameObject>();
            List<GameObject> tankOnSight = waypoint.GetComponent<WaypointEntity>().TankOnSight;
            return tankOnSight.Contains(tankAiComponent.TankEntity.Target) ? 1 : 0;
        }
        
    }
}