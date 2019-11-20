using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromFarthestGameObject : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> gameObjects = GameManager.Instance.GameObjects;
            gameObjects.Remove(tankAiComponent.gameObject);
            float distanceFromFarthestGameObject = Mathf.NegativeInfinity;
            if (gameObjects.Count == 0) return (int) distanceFromFarthestGameObject;
            distanceFromFarthestGameObject = gameObjects
                .Where(gameObject => gameObject != tankAiComponent.gameObject)
                .Select(gameObject => Vector3.Distance(gameObject.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestGameObject})
                .Max();
            return (int) distanceFromFarthestGameObject;
        }
        
    }
}