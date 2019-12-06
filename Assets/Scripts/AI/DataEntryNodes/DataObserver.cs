using System.Linq;
using Entities;
using Framework;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Utils;

namespace AI.DataEntryNodes {
    public class DataObserver : DataEntryNode {

        public FactionType FactionType = FactionType.Enemy;

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            GameObject data = GetData<GameObject>();
            if (FactionType == FactionType.All) {
                return data.transform.InLineOfView(tankAiComponent.transform,
                    tankAiComponent.TankEntity.TanksReference.Value
                        .Select(o => o.GetComponent<TankEntity>())
                        .Where(entity => entity != tankAiComponent.TankEntity)
                        .Select(entity => entity.transform).ToList(), tankAiComponent.TankEntity.CoverLayer).Count;
            }
            return data.transform.InLineOfView(tankAiComponent.transform,
                tankAiComponent.TankEntity.TanksReference.Value
                    .Select(o => o.GetComponent<TankEntity>())
                    .Where(entity => entity != tankAiComponent.TankEntity && tankAiComponent.TankEntity.GetFaction(entity) == FactionType)
                    .Select(entity => entity.transform).ToList(), tankAiComponent.TankEntity.CoverLayer).Count;
        }
        
    }
}