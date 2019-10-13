using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using Utils;

public class GameManager : Singleton<GameManager> {

    [Header("References")] 
    public GameObject WaypointPrefab;
    public Transform WaypointContent;
    public Transform BonusContent;
    public Transform GridStart;
    public Transform GridEnd;

    [Header("Parameters")] 
    public int GridXGap;
    public int GridZGap;
    public LayerMask CoverLayer;

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
        GenerateWaypointGrid();
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
                _waypointEntities.Add(instantiate.GetComponent<WaypointEntity>());
            }
        }
    }
    
    public void AddBonus(BonusEntity bonusEntity) {
        _bonusEntities.Add(bonusEntity);
    }
    
}
