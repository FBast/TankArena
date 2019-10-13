using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromFarthestWaypointNode : SimpleEntryNode {
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<WaypointEntity> waypointEntities = tankAiComponent.TankEntity.SeekWaypointInRadius();
            float distanceFromFarthestWaypoint = Mathf.NegativeInfinity;
            if (waypointEntities.Count == 0) return (int) distanceFromFarthestWaypoint;
            distanceFromFarthestWaypoint = waypointEntities
                .Select(waypoint => Vector3.Distance(waypoint.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestWaypoint})
                .Max();
            return (int) distanceFromFarthestWaypoint;
        }
        
    }
}