using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class IsBonusAvailable : SimpleEntryNode {
        protected override int ValueProvider(AbstractAIComponent context) {
            List<BonusEntity> bonusEntities = FindObjectsOfType<BonusEntity>().ToList();
            return bonusEntities.Count > 0 ? 1 : 0;
        }
    }
}