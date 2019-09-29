using UnityEngine;
using UnityEngine.AI;

namespace Entities {
    public class TankEntity : MonoBehaviour 
    {

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
        public TankEntity Target;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            CurrentHP = StartingHP;
        }

        private void Start() {
//            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
//            foreach (Vector3 vertex in triangulation.vertices) {
//                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//                primitive.transform.position = vertex;
//            }
        }

        private void Update() {
            Debug.DrawRay(CanonOut.position, CanonOut.forward * 100, Color.red);
            if (Target != null) {
                Vector3 newDir = Vector3.RotateTowards(Turret.forward, Target.transform.position - Turret.position, TurretSpeed * Time.deltaTime, 0.0f);
                Turret.rotation = Quaternion.LookRotation(newDir);
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
