using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.CollectionEntryNodes {
    public class OtherTanks : CollectionEntryNode {

        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> tanks = GameManager.Instance.TankEntities;
            tanks.Remove(tankAiComponent.gameObject);
            return new List<Object>(tanks.Where(entity => entity.gameObject != null)
                .Select(entity => entity.gameObject));
        }
        
    }

}