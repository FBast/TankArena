using System.Collections.Generic;
using System.Linq;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromNearestBonus : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> bonusEntities = tankAiComponent.TankEntity.BonusReference.Value;
            float distanceFromNearestBonus = Mathf.Infinity;
            if (bonusEntities.Count == 0) return (int) distanceFromNearestBonus;
            distanceFromNearestBonus = bonusEntities
                .Select(bonusEntity =>
                    Vector3.Distance(bonusEntity.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromNearestBonus})
                .Min();
            return (int) distanceFromNearestBonus;
        }

    }
}