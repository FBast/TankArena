using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

public class WaypointEntity : MonoBehaviour {

    public List<TankEntity> TankOnSight;
    public List<TankEntity> TankOnTheMap;

    private void Awake() {
        TankOnTheMap = FindObjectsOfType<TankEntity>().ToList();
    }

    private void Update() {
        TankOnSight = new List<TankEntity>();
        foreach (TankEntity tankEntity in TankOnTheMap) {
            if (!tankEntity) continue;
            if (Physics.Linecast(transform.position, tankEntity.transform.position)) continue;
            TankOnSight.Add(tankEntity);
            Debug.DrawLine(transform.position, tankEntity.transform.position, Color.blue);
        }
    }

}
