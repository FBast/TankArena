using System.Linq;
using Entities;
using Framework;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;

namespace AI.SimpleEntryNodes {
    public class AliveTank : SimpleEntryNode {

        public FactionType FactionType = FactionType.All;
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            if (FactionType == FactionType.All)
                return tankAiComponent.TankEntity.TanksReference.Value.Count;
            return tankAiComponent.TankEntity.TanksReference.Value
                .Count(o => o.GetComponent<TankEntity>().GetFaction(tankAiComponent.TankEntity) == FactionType);
        }
        
    }
}