using UnityEngine;

namespace Entities {
    public class ShellEntity : MonoBehaviour {

        public TankEntity TankEntityOwner;
        public GameObject ExplosionPrefab;
        [HideInInspector] public int Damage;

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<ShellEntity>()) return;
            TankEntity tankEntity = other.GetComponent<TankEntity>();
            if (tankEntity == TankEntityOwner) return;
            if (tankEntity) other.GetComponent<TankEntity>().Damage(Damage);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.Inverse(ExplosionPrefab.transform.rotation));
            Destroy(gameObject);
        }

    }
}
