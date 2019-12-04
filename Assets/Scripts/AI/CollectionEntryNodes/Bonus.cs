using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.CollectionEntryNodes {
    public class Bonus : CollectionEntryNode {

        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            return new List<Object>(tankAiComponent.TankEntity.BonusReference.Value
                .Select(entity => entity.gameObject));
        }
        
    }
}

