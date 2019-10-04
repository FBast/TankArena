using System.Collections.Generic;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class WaypointCover : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject waypoint = GetData<GameObject>();
            List<GameObject> tankOnSight = waypoint.GetComponent<WaypointEntity>().TankOnSight;
            if (tankOnSight.Contains(tankAiComponent.TankEntity.gameObject)) return tankOnSight.Count - 1;
            return tankOnSight.Count;
        }
        
    }
}