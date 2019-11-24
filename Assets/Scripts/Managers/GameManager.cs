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
        public GameObject SpinningCamera;
        public GameObject WaypointPrefab;
        public Transform WaypointContent;
        public Transform BonusContent;
        public Transform GridStart;
        public Transform GridEnd;
        public List<Transform> TeamAPositions;
        public List<Transform> TeamBPositions;
        public List<Transform> TeamCPositions;
        public List<Transform> TeamDPositions;

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
        private GameObject _tankCamera;
        private int _tankCameraIndex;
        
        private void Start() {
            _teamASettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamASettings);
            _teamBSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamBSettings);
            _teamCSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamCSettings);
            _teamDSettings = (List<TankSetting>) SceneManager.Instance.GetParam(Properties.Parameters.TeamDSettings);
            _tankEntities = FindObjectsOfType<TankEntity>().Select(entity => entity.gameObject).ToList();
            _waypointEntities = FindObjectsOfType<WaypointEntity>().Select(entity => entity.gameObject).ToList();
            _bonusEntities = FindObjectsOfType<BonusEntity>().Select(entity => entity.gameObject).ToList();
            _gameObjects = FindObjectsOfType<GameObject>().ToList();
            GenerateWaypointGrid();
            GenerateTanksForTeamFight();
        }

        private void Update() {
            if (Input.GetButtonDown("Fire1")) {
                _tankCameraIndex++;
                if (_tankCameraIndex >= TankEntities.Count) _tankCameraIndex = 0;
                CameraSwitch(TankEntities[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown("Fire2")) {
                _tankCameraIndex--;
                if (_tankCameraIndex < 0) _tankCameraIndex = TankEntities.Count - 1;
                CameraSwitch(TankEntities[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown("Fire3")) {
                _tankCamera.SetActive(false);
            }
        }

        private void CameraSwitch(GameObject newCamera) {
            if (_tankCamera)
                _tankCamera.SetActive(false);
            _tankCamera = newCamera;
            newCamera.SetActive(true);
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

        private void GenerateTanksForTeamFight() {
            GenerateTanks(_teamASettings, TeamAPositions, 1, Color.red);
            GenerateTanks(_teamBSettings, TeamBPositions, 2, Color.green);
            GenerateTanks(_teamCSettings, TeamCPositions, 3, Color.blue);
            GenerateTanks(_teamDSettings, TeamDPositions, 4, Color.yellow);
        }

        private void GenerateTanks(List<TankSetting> tankSettings, List<Transform> positions, int teamNumber, Color factionColor) {
            if (_teamASettings.Count > positions.Count)
                throw new Exception("Need more positions for tanks");
            for (int i = 0; i < tankSettings.Count; i++) {
                GameObject instantiate = Instantiate(TankPrefab, positions[i].position, Quaternion.identity);
                instantiate.GetComponent<TankEntity>().Init(tankSettings[i], teamNumber, factionColor);
                _tankEntities.Add(instantiate);
            }
        }

    }
}
