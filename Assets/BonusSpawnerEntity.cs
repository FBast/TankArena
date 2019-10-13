using Entities;
using UnityEngine;

public class BonusSpawnerEntity : MonoBehaviour {
    
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
            GameManager.Instance.AddBonus(_spawnedBonus.GetComponent<BonusEntity>());
            _timeSinceBonusUsed = 0;
            _spawnedNumber++;
        }
    }

}
