using UnityEngine;
using UnityEngine.AI;

namespace Entities {
    public class TankEntity : MonoBehaviour {

        [Header("References")] 
        public Transform CanonOut;
        public Transform Turret;
        public AudioSource ShotCharging;
        public AudioSource ShotFiring;
        
        [Header("Prefabs")]
        public GameObject ShellPrefab;
        public GameObject CanonShotPrefab;
        public GameObject TankExplosionPrefab;

        [Header("Parameters")] 
        public int CanonDamage;
        public int CanonPower;
        public int TurretSpeed;
        public int StartingHP;
        public int ReloadTime;
        
        [Header("Variables")]
        public int CurrentHP;
        public bool IsShellLoaded = true;
        public GameObject Target;
        public GameObject Destination;
        
        private NavMeshAgent _navMeshAgent;

        private bool _isAtDestination => _navMeshAgent.remainingDistance < Mathf.Infinity &&
                                          _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                                          _navMeshAgent.remainingDistance <= 0;

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            CurrentHP = StartingHP;
        }

        private void Update() {
            Debug.DrawRay(CanonOut.position, CanonOut.forward * 100, Color.red);
            if (Target) {
                Vector3 newDir = Vector3.RotateTowards(Turret.forward, Target.transform.position - Turret.position, TurretSpeed * Time.deltaTime, 0.0f);
                Turret.rotation = Quaternion.LookRotation(newDir);
            }
            if (Destination) {
                Vector3[] pathCorners = _navMeshAgent.path.corners;
                if (pathCorners.Length > 0) {
                    Debug.DrawLine(transform.position, pathCorners[0], Color.green);
                    for (int i = 1; i < pathCorners.Length - 1; i++) {
                        Debug.DrawLine(pathCorners[i - 1], pathCorners[i], Color.green);
                    }
                }
                _navMeshAgent.SetDestination(Destination.transform.position);
                if (_isAtDestination) Destination = null;
            }
        }

        public void Fire() {
            if (!IsShellLoaded) return;
            Instantiate(CanonShotPrefab, CanonOut.position, CanonShotPrefab.transform.rotation);
            ShotFiring.Play();
            GameObject instantiate = Instantiate(ShellPrefab, CanonOut.position, CanonOut.rotation);
            instantiate.GetComponent<Rigidbody>().AddForce(CanonOut.transform.forward * CanonPower, ForceMode.Impulse);
            instantiate.GetComponent<ShellEntity>().Damage = CanonDamage;
            IsShellLoaded = false;
            Invoke(nameof(Reload), ReloadTime);
        }

        public void Damage(int damage) {
            CurrentHP -= damage;
            if (CurrentHP > 0) return;
            Instantiate(TankExplosionPrefab, transform.position, TankExplosionPrefab.transform.rotation);
            Destroy(gameObject);
        }

        public void MoveTo(Vector3 position) {
            _navMeshAgent.SetDestination(position);
        }

        private void Reload() {
            IsShellLoaded = true;
            ShotCharging.Play();
        }

        public bool IsTankInRay() {
            RaycastHit hit;
            if (!Physics.Raycast(CanonOut.position, CanonOut.forward, out hit, Mathf.Infinity)) return false;
            return hit.transform.GetComponent<TankEntity>() != null;
        }

    }
}
