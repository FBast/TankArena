using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {
    
    public GameObject WaypointPrefab;
    public Transform WaypointContent;
    
    private void Start() {
        Cursor.visible = false;
//        NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
//        for (int i = 0; i < navMeshTriangulation.vertices.Length; i++) {
//            Instantiate(WaypointPrefab, navMeshTriangulation.vertices[i], Quaternion.identity, WaypointContent);
//        }
    }
}
