using System.Collections.Generic;
using System.Linq;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromBonus : SimpleEntryNode {

        public DistanceSorter DistanceSorter = DistanceSorter.Nearest;
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> bonusEntities = tankAiComponent.TankEntity.BonusReference.Value;
            float distance = 0;
            if (DistanceSorter == DistanceSorter.Nearest) {
                distance = Mathf.Infinity;
                if (bonusEntities.Count == 0) return (int) distance;
                distance = bonusEntities
                    .Select(bonusEntity =>
                        Vector3.Distance(bonusEntity.transform.position, tankAiComponent.transform.position))
                    .Concat(new[] {distance})
                    .Min();
            }
            if (DistanceSorter == DistanceSorter.Farthest) {
                distance = Mathf.NegativeInfinity;
                if (bonusEntities.Count == 0) return (int) distance;
                distance = bonusEntities
                    .Select(bonus => Vector3.Distance(bonus.transform.position, tankAiComponent.transform.position))
                    .Concat(new[] {distance})
                    .Max();
            }
            return (int) distance;
        }

    }
}