using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.CollectionEntryNodes {
    public class WaypointsNodes : CollectionEntryNode {

        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            List<WaypointEntity> waypoints = FindObjectsOfType<WaypointEntity>().ToList();
            return new List<Object>(waypoints.Select(entity => entity.gameObject));
        }
        
    }
}