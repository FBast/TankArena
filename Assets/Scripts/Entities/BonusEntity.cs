using UnityEngine;

namespace Entities {
    public class BonusEntity : MonoBehaviour {

        public int SpeedRotation;
        public GameObject BonusExplosionPrefab;
        public int Healing;

        private void FixedUpdate() {
            transform.Rotate(0, Time.fixedDeltaTime * SpeedRotation, 0, Space.World);
        }

        private void OnTriggerEnter(Collider other) {
            Instantiate(BonusExplosionPrefab, transform.position, Quaternion.identity);
            if (other.gameObject.GetComponent<TankEntity>()) {
                other.gameObject.GetComponent<TankEntity>().Heal(Healing);
            }
            Destroy(gameObject);
        }
    }
}
