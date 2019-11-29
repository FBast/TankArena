using System.Collections.Generic;
using System.Linq;
using Data;
using Entities;
using Framework;
using SOReferences.GameObjectListReference;
using SOReferences.GameReference;
using SOReferences.MatchReference;
using UnityEngine;
using Utils;

namespace Managers {
    public class GameManager : Singleton<GameManager> {

        [Header("Prefabs")] 
        public GameObject TankPrefab;

        [Header("References")] 
        public List<Transform> TeamPositions;

        [Header("SO References")] 
        public GameReference CurrentGameReference;
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
            TanksReference.Value = new List<GameObject>();
            CurrentGameReference.Value.NextMatch();
            CurrentMatchReference.Value = CurrentGameReference.Value.CurrentMatch;
            for (int i = 0; i < CurrentMatchReference.Value.Teams.Count; i++) {
                GenerateTankTeam(CurrentMatchReference.Value.Teams[i], TeamPositions[i]);
            }
        }

        private void Update() {
            if (Input.GetButtonDown(Properties.Inputs.LeftClick)) {
                _tankCameraIndex++;
                if (_tankCameraIndex >= TanksReference.Value.Count) _tankCameraIndex = 0;
                CameraSwitch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown(Properties.Inputs.RightClick)) {
                _tankCameraIndex--;
                if (_tankCameraIndex < 0) _tankCameraIndex = TanksReference.Value.Count - 1;
                CameraSwitch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown(Properties.Inputs.WheelClick)) {
                _tankCamera.SetActive(false);
            }
        }

        private void CameraSwitch(GameObject newCamera) {
            if (_tankCamera)
                _tankCamera.SetActive(false);
            _tankCamera = newCamera;
            newCamera.SetActive(true);
        }

        private void GenerateTankTeam(Team team, Transform centerPosition) {
            foreach (TankSetting tankSetting in team.TankSettings) {
                GameObject instantiate = Instantiate(TankPrefab, centerPosition.position, Quaternion.identity);
                instantiate.GetComponent<TankEntity>().Init(tankSetting, team);
                TanksReference.Value.Add(instantiate);
            }
        }

    }
}
