using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI {
    public class TankEntity : MonoBehaviour 
    {

        [Header("References")] 
        public Transform CanonOut;
        public GameObject ShellPrefab;

        [Header("Parameters")] 
        public int CanonPower;
        public int StartingHP;
        public int CurrentHP;
        public int ReloadTime;
        public bool IsShellLoaded = true;

        private NavMeshAgent _navMeshAgent;

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Fire() {
            if (!IsShellLoaded) return;
            GameObject instantiate = Instantiate(ShellPrefab, CanonOut.position, CanonOut.rotation);
            instantiate.GetComponent<Rigidbody>().AddForce(CanonOut.transform.forward * CanonPower, ForceMode.Impulse);
            IsShellLoaded = false;
            Invoke(nameof(Reload), ReloadTime);
        }

        public void MoveTo(Vector3 position) {
            _navMeshAgent.SetDestination(position);
        }

        private void Reload() {
            IsShellLoaded = true;
        }

        public bool IsTankInRay() {
            RaycastHit hit;
            if (!Physics.Raycast(CanonOut.position, CanonOut.forward, out hit, Mathf.Infinity)) return false;
            return hit.transform.GetComponent<TankEntity>() != null;
        }

    }
}
