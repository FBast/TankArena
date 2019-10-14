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
            List<GameObject> waypointGameObjects = tankAiComponent.TankEntity.SeekWaypointInRadius();
            float distanceFromFarthestWaypoint = Mathf.NegativeInfinity;
            if (waypointGameObjects.Count == 0) return (int) distanceFromFarthestWaypoint;
            distanceFromFarthestWaypoint = waypointGameObjects
                .Select(waypoint => Vector3.Distance(waypoint.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestWaypoint})
                .Max();
            return (int) distanceFromFarthestWaypoint;
        }
        
    }
}