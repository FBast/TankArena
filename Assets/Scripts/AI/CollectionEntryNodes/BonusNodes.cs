using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.CollectionEntryNodes {
    public class BonusNodes : CollectionEntryNode {

        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            List<BonusEntity> bonus = FindObjectsOfType<BonusEntity>().ToList();
            return new List<Object>(bonus.Select(entity => entity.gameObject));
        }
        
    }
}

