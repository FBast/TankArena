using System.Collections.Generic;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class WaypointCover : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject waypoint = GetData<GameObject>();
            List<TankEntity> tankOnSight = waypoint.GetComponent<WaypointEntity>().TankOnSight;
            if (tankOnSight.Contains(tankAiComponent.TankEntity)) return tankOnSight.Count - 1;
            return tankOnSight.Count;
        }
        
    }
}