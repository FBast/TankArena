using System.Collections.Generic;
using SOReferences.GameObjectListReference;
using UnityEngine;

namespace Framework {
    public class BonusSpawner : MonoBehaviour {

        [Header("Prefabs")]
        public GameObject BonusPrefab;
        
        [Header("Parameters")]
        public string BonusName;
        public int SpawnRate;
        public int SpawnNumber;

        [Header("SO References")] 
        public GameObjectListReference BonusReference;
        
        private GameObject _spawnedBonus;
        private float _timeSinceBonusUsed;
        private int _spawnedNumber;

        private void Awake() {
            BonusReference.Value = new List<GameObject>();
        }

        private void Update() {
            if (_spawnedBonus || _spawnedNumber >= SpawnNumber) return;
            _timeSinceBonusUsed += Time.deltaTime;
            if (_timeSinceBonusUsed > SpawnRate) {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y = BonusPrefab.transform.position.y;
                _spawnedBonus = Instantiate(BonusPrefab, spawnPosition, Quaternion.identity, transform);
                _spawnedBonus.name = BonusName;
                BonusReference.Value.Add(_spawnedBonus);
                _timeSinceBonusUsed = 0;
                _spawnedNumber++;
            }
        }

    }
}
