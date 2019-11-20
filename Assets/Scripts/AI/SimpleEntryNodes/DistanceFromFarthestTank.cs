using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromFarthestTank : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> tankEntities = GameManager.Instance.TankEntities;
            float distanceFromFarthestTank = Mathf.NegativeInfinity;
            if (tankEntities.Count == 0) return (int) distanceFromFarthestTank;
            distanceFromFarthestTank = tankEntities
                .Where(go => go != tankAiComponent.gameObject)
                .Select(tankEntity => Vector3.Distance(tankEntity.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestTank})
                .Max();
            return (int) distanceFromFarthestTank;
        }
        
    }
}