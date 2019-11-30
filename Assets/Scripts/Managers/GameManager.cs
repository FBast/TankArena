using System.Collections.Generic;
using System.Linq;
using SOReferences.GameObjectListReference;
using SOReferences.GameReference;
using SOReferences.MatchReference;
using UnityEngine;
using Utils;

namespace Managers {
    public class GameManager : Singleton<GameManager> {

        [Header("SO References")] 
        public GameObjectListReference BonusReference;
        public GameObjectListReference TanksReference;
        public GameObjectListReference WaypointsReference;

        [Header("Parameters")] 
        public LayerMask CoverLayer;

        public List<GameObject> TankEntities => TanksReference.Value.Where(go => go != null).ToList();
        public List<GameObject> WaypointEntities => WaypointsReference.Value.Where(go => go != null).ToList();
        public List<GameObject> BonusEntities => BonusReference.Value.Where(go => go != null).ToList();
        public List<GameObject> GameObjects => _gameObjects.Where(go => go != null).ToList();

        private List<GameObject> _gameObjects = new List<GameObject>();

        private void Awake() {
            _gameObjects = FindObjectsOfType<GameObject>().ToList();
        }

    }
}
