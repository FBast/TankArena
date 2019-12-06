using System.Collections.Generic;
using System.Linq;
using Entities;
using Framework;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromTank : SimpleEntryNode {

        public FactionType FactionType = FactionType.All;
        public DistanceSorter DistanceSorter = DistanceSorter.Nearest;
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> tankEntities = tankAiComponent.TankEntity.TanksReference.Value.ToList();
            tankEntities.Remove(tankAiComponent.gameObject);
            if (FactionType != FactionType.All)
                tankEntities.RemoveAll(o => o.GetComponent<TankEntity>().GetFaction(tankAiComponent.TankEntity) != FactionType);
            float distance = 0;
            if (DistanceSorter == DistanceSorter.Nearest) {
                distance = Mathf.Infinity;
                if (tankEntities.Count == 0) return (int) distance;
                distance = tankEntities
                        .Select(tankEntity => Vector3.Distance(tankEntity.transform.position, tankAiComponent.transform.position))
                        .Concat(new[] {distance})
                        .Min();
            }
            if (DistanceSorter == DistanceSorter.Farthest) {
                distance = Mathf.NegativeInfinity;
                if (tankEntities.Count == 0) return (int) distance;
                distance = tankEntities
                    .Where(go => go != tankAiComponent.gameObject)
                    .Select(tankEntity => Vector3.Distance(tankEntity.transform.position, tankAiComponent.transform.position))
                    .Concat(new[] {distance})
                    .Max();
            }
            return (int) distance;
        }
        
    }
}