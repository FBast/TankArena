using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromFarthestTankNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<TankEntity> tankEntities = FindObjectsOfType<TankEntity>().ToList();
            tankEntities.Remove(tankAiComponent.TankEntity);
            float distanceFromFarthestTank = Mathf.NegativeInfinity;
            if (tankEntities.Count == 0) return (int) distanceFromFarthestTank;
            distanceFromFarthestTank = tankEntities
                .Select(tankEntity => Vector3.Distance(tankEntity.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestTank})
                .Max();
            return (int) distanceFromFarthestTank;
        }
        
    }
}