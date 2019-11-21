using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AI.CollectionEntryNodes {
    public class OtherTanks : CollectionEntryNode {

        public enum FactionType {
            Ally,
            Enemy,
            All
        }

        public FactionType Faction = FactionType.Enemy;
        
        protected override List<Object> CollectionProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> tanks = GameManager.Instance.TankEntities;
            tanks.Remove(tankAiComponent.gameObject);
            switch (Faction) {
                case FactionType.Ally:
                    tanks.RemoveAll(go =>
                        go.GetComponent<TankEntity>().TeamNumber != tankAiComponent.TankEntity.TeamNumber);
                    break;
                case FactionType.Enemy:
                    tanks.RemoveAll(go =>
                        go.GetComponent<TankEntity>().TeamNumber == tankAiComponent.TankEntity.TeamNumber);
                    break;
                case FactionType.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new List<Object>(tanks.Where(go => go != null)
                .Select(entity => entity.gameObject));
        }
        
    }

}