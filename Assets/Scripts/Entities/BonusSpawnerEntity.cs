using Managers;
using UnityEngine;

namespace Entities
{
    public class BonusSpawnerEntity : MonoBehaviour {

        public string BonusName;
        public GameObject BonusPrefab;
        public int SpawnRate;
        public int SpawnNumber;
    
        private GameObject _spawnedBonus;
        private float _timeSinceBonusUsed;
        private int _spawnedNumber;

        private void Update() {
            if (_spawnedBonus || _spawnedNumber >= SpawnNumber) return;
            _timeSinceBonusUsed += Time.deltaTime;
            if (_timeSinceBonusUsed > SpawnRate) {
                Vector3 spawnPosition = transform.position;
                spawnPosition.y = BonusPrefab.transform.position.y;
                _spawnedBonus = Instantiate(BonusPrefab, spawnPosition, Quaternion.identity, GameManager.Instance.BonusContent);
                _spawnedBonus.name = BonusName;
                GameManager.Instance.AddBonus(_spawnedBonus);
                _timeSinceBonusUsed = 0;
                _spawnedNumber++;
            }
        }

    }
}
