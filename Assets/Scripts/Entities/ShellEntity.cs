using UnityEngine;

namespace Entities {
    public class ShellEntity : MonoBehaviour {

        public GameObject ExplosionPrefab;
        [HideInInspector] public int Damage;

        private void OnTriggerEnter(Collider other) {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.Inverse(ExplosionPrefab.transform.rotation));
            if (other.gameObject.GetComponent<TankEntity>() != null)
                other.gameObject.GetComponent<TankEntity>().Damage(Damage);
            Destroy(gameObject);
        }

    }
}
