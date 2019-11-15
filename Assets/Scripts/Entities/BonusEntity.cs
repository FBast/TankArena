using UnityEngine;

namespace Entities {
    public class BonusEntity : MonoBehaviour {
        
        public GameObject BonusExplosionPrefab;
        public int Healing;
        
        private void OnTriggerEnter(Collider other) {
            Instantiate(BonusExplosionPrefab, transform.position, Quaternion.identity);
            if (other.gameObject.GetComponent<TankEntity>()) {
                other.gameObject.GetComponent<TankEntity>().Heal(Healing);
            }
            Destroy(gameObject);
        }
    }
}
