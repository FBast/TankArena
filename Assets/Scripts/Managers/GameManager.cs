using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UI;
using UnityEngine;
using Utils;

namespace Managers {
    public class GameManager : Singleton<GameManager> {

        [Header("Prefabs")] 
        public GameObject TankPrefab;
    
        [Header("References")] 
        public GameObject WaypointPrefab;
        public Transform WaypointContent;
        public Transform BonusContent;
        public Transform GridStart;
        public Transform GridEnd;
        public List<Transform> TankMeleePositions;
        public List<Transform> TankDuelPositions;

        [Header("External References")] 
        public TankLifeUI TankLifeUi;
    
        [Header("Parameters")] 
        public int GridXGap;
        public int GridZGap;
        public LayerMask CoverLayer;

        public List<GameObject> TankEntities => _tankEntities.Where(go => go != null).ToList();
        public List<GameObject> WaypointEntities => _waypointEntities.Where(go => go != null).ToList();
        public List<GameObject> BonusEntities => _bonusEntities.Where(go => go != null).ToList();
        public List<GameObject> GameObjects => _gameObjects.Where(go => go != null).ToList();

        private List<GameObject> _tankEntities = new List<GameObject>();
        private List<GameObject> _waypointEntities = new List<GameObject>();
        private List<GameObject> _bonusEntities = new List<GameObject>();
        private List<GameObject> _gameObjects = new List<GameObject>();

        private List<TankSetting> _tankSettings;
        
        private void Start() {
            _tankSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TankSettings);
            _tankEntities = FindObjectsOfType<TankEntity>().Select(entity => entity.gameObject).ToList();
            _waypointEntities = FindObjectsOfType<WaypointEntity>().Select(entity => entity.gameObject).ToList();
            _bonusEntities = FindObjectsOfType<BonusEntity>().Select(entity => entity.gameObject).ToList();
            _gameObjects = FindObjectsOfType<GameObject>().ToList();
            GenerateWaypointGrid();
            GenerateTanks();
        }

        public void GenerateWaypointGrid() {
            float positionX = GridEnd.position.x - GridStart.position.x;
            float positionZ = GridEnd.position.z - GridStart.position.z;
            int xSign = positionX < 0 ? -1 : 1;
            int ySign = positionZ < 0 ? -1 : 1;
            for (int i = 0; i < Mathf.Abs(positionX) / GridXGap; i++) {
                for (int j = 0; j < Mathf.Abs(positionZ) / GridZGap; j++) {
                    Vector3 position = new Vector3(GridStart.position.x + i * GridXGap * xSign, 
                        0, GridStart.position.z + j * GridZGap * ySign);
                    if (!position.IsPositionOnNavMesh()) continue;
                    GameObject instantiate = Instantiate(WaypointPrefab, position, Quaternion.identity, WaypointContent);
                    instantiate.name = (int) instantiate.transform.position.x + ", " + (int) instantiate.transform.position.z;
                    _waypointEntities.Add(instantiate);
                }
            }
        }
    
        public void AddBonus(GameObject bonusGameObject) {
            _bonusEntities.Add(bonusGameObject);
        }

        public void GenerateTanks() {
            if (_tankSettings.Count > TankMeleePositions.Count)
                throw new Exception("Need more positions for tanks");
            for (int i = 0; i < _tankSettings.Count; i++) {
                Transform tankPosition = TankMeleePositions[i];
                GameObject instantiate = Instantiate(TankPrefab, tankPosition.position, Quaternion.identity);
                instantiate.GetComponent<TankEntity>().InitTank(_tankSettings[i]);
                _tankEntities.Add(instantiate);
            }
            TankLifeUi.UpdateUI(_tankEntities);
        }
    
    }
}
