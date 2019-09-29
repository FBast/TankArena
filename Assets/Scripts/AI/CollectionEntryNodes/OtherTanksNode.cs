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
            List<TankEntity> tanks = FindObjectsOfType<TankEntity>().ToList();
            if (tanks.Contains(tankAiComponent.TankEntity)) tanks.Remove(tankAiComponent.TankEntity);
            return new List<Object>(tanks.Select(entity => entity.gameObject));
        }
        
    }

}