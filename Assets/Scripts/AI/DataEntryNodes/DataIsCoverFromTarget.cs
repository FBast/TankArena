using System.Linq;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Utils;

namespace AI.DataEntryNodes {
    public class DataIsCoverFromTarget : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            return data.transform.InLineOfView(tankAiComponent.transform,
                       GameManager.Instance.TankEntities
                           .Where(entity => entity.gameObject == tankAiComponent.TankEntity.Target)
                           .Select(entity => entity.transform)
                           .ToList(), GameManager.Instance.CoverLayer).Count == 0 ? 1 : 0;
        }
        
    }
}