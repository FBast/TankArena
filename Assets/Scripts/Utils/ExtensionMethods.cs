using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Utils {
    public static class ExtensionMethods {

        public static List<Transform> InLineOfView(this Transform trans, Transform Hider, List<Transform> Seekers, LayerMask layerMask) {
            List<Transform> inLineOfView = new List<Transform>();
            foreach (Transform seeker in Seekers) {
                if (Physics.Linecast(trans.position + Hider.PivotToCenter(), seeker.position + seeker.PivotToCenter(), layerMask)) continue;
                inLineOfView.Add(seeker);
            }
            return inLineOfView;
        }

        public static Vector3 PivotToCenter(this Transform trans) {
            Vector3 distance = Vector3.zero;
            if (trans.GetComponent<Collider>())
                distance = trans.GetComponent<Collider>().bounds.center - trans.position;
            return distance;
        }
        
        public static bool IsPositionOnNavMesh(this Vector3 position) {
            const float onMeshThreshold = 1;
            // Check for nearest point on navmesh to agent, within onMeshThreshold
            return NavMesh.SamplePosition(position, out _, onMeshThreshold, NavMesh.AllAreas);
        }
        
    }
}