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
            return new List<Object>(GameManager.Instance.BonusEntities.Select(entity => entity.gameObject));
        }
        
    }
}

