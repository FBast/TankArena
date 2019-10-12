using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class IsBonusAvailable : SimpleEntryNode {
        protected override int ValueProvider(AbstractAIComponent context) {
            return GameManager.Instance.BonusEntities.Count > 0 ? 1 : 0;
        }
    }
}