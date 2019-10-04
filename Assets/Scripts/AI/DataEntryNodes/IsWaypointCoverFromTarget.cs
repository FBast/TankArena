using System.Collections.Generic;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class IsWaypointCoverFromTarget : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject waypoint = GetData<GameObject>();
            List<GameObject> tankOnSight = waypoint.GetComponent<WaypointEntity>().TankOnSight;
            return tankOnSight.Contains(tankAiComponent.TankEntity.Target) ? 0 : 1;
        }
        
    }
}