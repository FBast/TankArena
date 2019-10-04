using System.Collections.Generic;
using UnityEngine;

namespace Entities {
    public class WaypointEntity : MonoBehaviour {

        public List<GameObject> TankOnSight;

        private void Update() {
            TankOnSight = new List<GameObject>();
            foreach (TankEntity tankEntity in FindObjectsOfType<TankEntity>()) {
                if (Physics.Linecast(transform.position, tankEntity.transform.position)) continue;
                TankOnSight.Add(tankEntity.gameObject);
                Debug.DrawLine(transform.position, tankEntity.transform.position, Color.blue);
            }
        }

    }
}
