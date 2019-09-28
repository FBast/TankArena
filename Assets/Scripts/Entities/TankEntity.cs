using UnityEngine;
using UnityEngine.AI;

namespace Entities {
    public class TankEntity : MonoBehaviour 
    {

        [Header("References")] 
        public Transform CanonOut;
        public GameObject ShellPrefab;
        public GameObject CanonShotPrefab;
        public GameObject TankExplosionPrefab;

        [Header("Parameters")] 
        public int CanonDamage;
        public int CanonPower;
        public int StartingHP;
        public int ReloadTime;
        
        [Header("Variables")]
        public int CurrentHP;
        public bool IsShellLoaded = true;

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

        public void Fire() {
            if (!IsShellLoaded) return;
            Instantiate(CanonShotPrefab, CanonOut.position, CanonShotPrefab.transform.rotation);
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
        }

        public bool IsTankInRay() {
            RaycastHit hit;
            if (!Physics.Raycast(CanonOut.position, CanonOut.forward, out hit, Mathf.Infinity)) return false;
            return hit.transform.GetComponent<TankEntity>() != null;
        }

    }
}
