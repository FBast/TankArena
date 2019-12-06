using System.Collections.Generic;
using System.Linq;
using Entities;
using Framework;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AI.CollectionEntryNodes {
    public class OtherTanks : CollectionEntryNode {

        public FactionType Faction = FactionType.Enemy;
        
        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> tanks = tankAiComponent.TankEntity.TanksReference.Value.ToList();
            tanks.Remove(tankAiComponent.gameObject);
            if (Faction != FactionType.All)
                tanks.RemoveAll(o => o.GetComponent<TankEntity>().GetFaction(tankAiComponent.TankEntity) != Faction);
            return new List<Object>(tanks.Where(go => go != null)
                .Select(entity => entity.gameObject));
        }
        
    }

}