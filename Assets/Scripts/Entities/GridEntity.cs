using System.Collections.Generic;
using SOReferences.GameObjectListReference;
using UnityEngine;
using Utils;

namespace Entities {
    public class GridEntity : MonoBehaviour {

        [Header("Prefabs")] 
        public GameObject WaypointPrefab;

        [Header("Internal References")]
        public Transform GridStart;
        public Transform GridEnd;
        public Transform WaypointContent;

        [Header("SO References")] 
        public GameObjectListReference WaypointsReference;
        
        [Header("Parameters")] 
        public int GridXGap;
        public int GridZGap;
        
        private void Start() {
            WaypointsReference.Value = new List<GameObject>();
            GenerateWaypointGrid();
        }

        private void GenerateWaypointGrid() {

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
                    instantiate.name = (int) position.x + ", " + (int) position.z;
                    WaypointsReference.Value.Add(instantiate);
                }
            }
        }

    }
}
