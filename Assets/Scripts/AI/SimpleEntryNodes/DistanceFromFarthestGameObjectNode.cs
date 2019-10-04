using System.Collections.Generic;
using System.Linq;
using Entities;
using NodeUtilityAi;
using NodeUtilityAi.Nodes;
using UnityEngine;

namespace AI.SimpleEntryNodes {
    public class DistanceFromFarthestGameObjectNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            TankAIComponent tankAiComponent = (TankAIComponent) context;
            List<GameObject> gameObjects = FindObjectsOfType<GameObject>().ToList();
            gameObjects.Remove(tankAiComponent.gameObject);
            float distanceFromFarthestGameObject = Mathf.NegativeInfinity;
            if (gameObjects.Count == 0) return (int) distanceFromFarthestGameObject;
            distanceFromFarthestGameObject = gameObjects
                .Select(gameObject => Vector3.Distance(gameObject.transform.position, tankAiComponent.transform.position))
                .Concat(new[] {distanceFromFarthestGameObject})
                .Max();
            return (int) distanceFromFarthestGameObject;
        }
        
    }
}