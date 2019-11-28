using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entities;
using Framework;
using SOReferences.GameObjectListReference;
using SOReferences.MatchReference;
using UnityEngine;
using Utils;

namespace Managers {
    public class GameManager : Singleton<GameManager> {

        [Header("Prefabs")] 
        public GameObject TankPrefab;

        [Header("References")] 
        public List<Transform> TeamAPositions;
        public List<Transform> TeamBPositions;
        public List<Transform> TeamCPositions;
        public List<Transform> TeamDPositions;

        [Header("SO References")] 
        public MatchReference CurrentMatchReference;
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
        private GameObject _tankCamera;
        private int _tankCameraIndex;

        private void Awake() {
            _gameObjects = FindObjectsOfType<GameObject>().ToList();
        }

        private void Start() {
            GenerateTanksForTeamFight();
        }

        private void Update() {
            if (Input.GetButtonDown("Fire1")) {
                _tankCameraIndex++;
                if (_tankCameraIndex >= TanksReference.Value.Count) _tankCameraIndex = 0;
                CameraSwitch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown("Fire2")) {
                _tankCameraIndex--;
                if (_tankCameraIndex < 0) _tankCameraIndex = TanksReference.Value.Count - 1;
                CameraSwitch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
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

        private void GenerateTanksForTeamFight() {
            TanksReference.Value = new List<GameObject>();
            GenerateTanks(CurrentMatchReference.Value.Teams[0], TeamAPositions);
            GenerateTanks(CurrentMatchReference.Value.Teams[1], TeamBPositions);
            GenerateTanks(CurrentMatchReference.Value.Teams[2], TeamCPositions);
            GenerateTanks(CurrentMatchReference.Value.Teams[3], TeamDPositions);
        }

        private void GenerateTanks(Team team, List<Transform> positions) {
            if (team.TankSettings.Count > positions.Count)
                throw new Exception("Need more positions for tanks");
            for (int i = 0; i < team.TankSettings.Count; i++) {
                GameObject instantiate = Instantiate(TankPrefab, positions[i].position, Quaternion.identity);
                instantiate.GetComponent<TankEntity>().Init(team.TankSettings[i], team);
                TanksReference.Value.Add(instantiate);
            }
        }

    }
}
