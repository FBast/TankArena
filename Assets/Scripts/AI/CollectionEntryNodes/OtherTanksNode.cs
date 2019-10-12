using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.CollectionEntryNodes {
    public class OtherTanksNode : CollectionEntryNode {

        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<TankEntity> tanks = GameManager.Instance.TankEntities;
            tanks.Remove(tankAiComponent.TankEntity);
            return new List<Object>(tanks.Where(entity => entity.gameObject != null)
                .Select(entity => entity.gameObject));
        }
        
    }

}