using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
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
        public List<Transform> TeamAPositions;
        public List<Transform> TeamBPositions;
        public List<Transform> TeamCPositions;
        public List<Transform> TeamDPositions;
        public List<Transform> FfaPositions;

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
        private List<TankSetting> _teamASettings;
        private List<TankSetting> _teamBSettings;
        private List<TankSetting> _teamCSettings;
        private List<TankSetting> _teamDSettings;
        private List<TankSetting> _ffaSettings;
        private int _tankNumber;

        private void Start() {
            _teamASettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamASettings);
            _teamBSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamBSettings);
            _teamCSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamCSettings);
            _teamDSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamDSettings);
            _ffaSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.FFASettings);
            _tankNumber = (int) SceneManager.Instance.GetParam(Properties.Parameters.TankNumber);
            _tankEntities = FindObjectsOfType<TankEntity>().Select(entity => entity.gameObject).ToList();
            _waypointEntities = FindObjectsOfType<WaypointEntity>().Select(entity => entity.gameObject).ToList();
            _bonusEntities = FindObjectsOfType<BonusEntity>().Select(entity => entity.gameObject).ToList();
            _gameObjects = FindObjectsOfType<GameObject>().ToList();
            GenerateWaypointGrid();
            GenerateTanksForDuel();
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

        private void GenerateTanksForDuel() {
            GenerateTanks(_teamASettings, TeamAPositions, 1);
            GenerateTanks(_teamBSettings, TeamBPositions, 2);
        }

        private void GenerateTanksForTeamFight() {
            GenerateTanks(_teamASettings, TeamAPositions, 1);
            GenerateTanks(_teamBSettings, TeamBPositions, 2);
            GenerateTanks(_teamCSettings, TeamCPositions, 3);
            GenerateTanks(_teamDSettings, TeamDPositions, 4);
        }

        private void GenerateTanks(List<TankSetting> tankSettings, List<Transform> positions, int teamNumber) {
            if (_teamASettings.Count * _tankNumber > positions.Count)
                throw new Exception("Need more positions for tanks");
            List<TankSetting> allTankSettings = new List<TankSetting>();
            foreach (TankSetting tankSetting in tankSettings) {
                for (int i = 0; i < _tankNumber; i++) {
                    allTankSettings.Add(tankSetting);
                }
            }
            for (int i = 0; i < allTankSettings.Count; i++) {
                GameObject instantiate = Instantiate(TankPrefab, positions[i].position, Quaternion.identity);
                instantiate.GetComponent<TankEntity>().InitTank(allTankSettings[i], teamNumber);
                _tankEntities.Add(instantiate);
            }
        }

    }
}
