using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

public class WaypointEntity : MonoBehaviour {

    public List<GameObject> TankOnSight;
    public List<GameObject> TankOnTheMap;

    private void Awake() {
        TankOnTheMap = FindObjectsOfType<TankEntity>().Select(entity => entity.gameObject).ToList();
    }

    private void Update() {
        TankOnSight = new List<GameObject>();
        foreach (GameObject tankGameObject in TankOnTheMap) {
            if (!tankGameObject) continue;
            if (Physics.Linecast(transform.position, tankGameObject.transform.position)) continue;
            TankOnSight.Add(tankGameObject);
            Debug.DrawLine(transform.position, tankGameObject.transform.position, Color.blue);
        }
    }

}
