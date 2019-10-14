using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.DataEntryNodes {
    public class DataCurrentHp : DataEntryNode {
        
        protected override int ValueProvider(AbstractAIComponent context) {
            GameObject target = GetData<GameObject>();
            return target.GetComponent<TankEntity>().CurrentHP;
        }
        
    }
}