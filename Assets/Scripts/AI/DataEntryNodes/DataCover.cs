using System.Linq;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Utils;

namespace AI.DataEntryNodes {
    public class DataCover : DataEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            return data.transform.InLineOfView(tankAiComponent.transform,
                GameManager.Instance.TankEntities
                    .Where(entity => entity != tankAiComponent.TankEntity)
                    .Select(entity => entity.transform).ToList(), GameManager.Instance.CoverLayer).Count;
        }
        
    }
}