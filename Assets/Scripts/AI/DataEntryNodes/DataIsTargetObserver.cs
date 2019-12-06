using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Utils;

namespace AI.DataEntryNodes {
    public class DataIsTargetObserver : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            return data.transform.InLineOfView(tankAiComponent.transform,
                       tankAiComponent.TankEntity.TanksReference.Value
                           .Select(o => o.GetComponent<TankEntity>())
                           .Where(entity => entity.gameObject == tankAiComponent.TankEntity.Target)
                           .Select(entity => entity.transform)
                           .ToList(), tankAiComponent.TankEntity.CoverLayer).Count == 0 ? 1 : 0;
        }
        
    }
}