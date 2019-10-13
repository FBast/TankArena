using System;
using Entities;
using UnityEngine;

public class SphereOverlapTest : MonoBehaviour {

    public int OverlapRadius = 50;
    public LayerMask layerMask;
    public QueryTriggerInteraction QueryTriggerInteraction;

    private void Update() {
        foreach (Collider col in Physics.OverlapSphere(transform.position, OverlapRadius, layerMask, QueryTriggerInteraction)) {
            Debug.Log("Collide with " + col.gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, OverlapRadius);
    }
    
}
