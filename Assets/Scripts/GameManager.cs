using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    
    public GameObject WaypointPrefab;
    public Transform WaypointContent;
    public List<TankEntity> TankEntities => _tankEntities.Where(entity => entity != null).ToList();
    public List<WaypointEntity> WaypointEntities => _waypointEntities.Where(entity => entity != null).ToList();
    public List<BonusEntity> BonusEntities => _bonusEntities.Where(entity => entity != null).ToList();
    public List<GameObject> GameObjects => _gameObjects.Where(o => o != null).ToList();

    private List<TankEntity> _tankEntities = new List<TankEntity>();
    private List<WaypointEntity> _waypointEntities = new List<WaypointEntity>();
    private List<BonusEntity> _bonusEntities = new List<BonusEntity>();
    private List<GameObject> _gameObjects = new List<GameObject>();
    
    private void Start() {
        _tankEntities = FindObjectsOfType<TankEntity>().ToList();
        _waypointEntities = FindObjectsOfType<WaypointEntity>().ToList();
        _bonusEntities = FindObjectsOfType<BonusEntity>().ToList();
        _gameObjects = FindObjectsOfType<GameObject>().ToList();
    }
    
}
