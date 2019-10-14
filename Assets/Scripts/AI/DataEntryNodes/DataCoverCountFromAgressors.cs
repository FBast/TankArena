using System.Linq;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Utils;

namespace AI.DataEntryNodes {
    public class DataCoverCountFromAgressors : DataEntryNode {
        
        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            return data.transform.InLineOfView(tankAiComponent.transform, tankAiComponent.TankEntity.Aggressors
                       .Select(go => go.transform).ToList(), GameManager.Instance.CoverLayer).Count;
        }
        
    }

}