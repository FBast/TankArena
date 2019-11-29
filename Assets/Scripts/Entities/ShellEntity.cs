using SOReferences.GameReference;
using UnityEngine;

namespace Entities {
    public class ShellEntity : MonoBehaviour {

        [Header("Prefabs")]
        public GameObject ExplosionPrefab;

        [Header("SO References")] 
        public GameReference CurrentGameReference;
        
        [HideInInspector] public TankEntity TankEntityOwner;
        [HideInInspector] public int Damage;

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<ShellEntity>()) return;
            TankEntity tankEntity = other.GetComponent<TankEntity>();
            if (tankEntity == TankEntityOwner) return;
            if (tankEntity) {
                if (tankEntity.CurrentHP - Damage < 0)
                    CurrentGameReference.Value.CurrentMatch.TeamStats[TankEntityOwner.Team].KillCount++;
                tankEntity.Damage(Damage);
                CurrentGameReference.Value.CurrentMatch.TeamStats[TankEntityOwner.Team].DamageDone += Damage;
            }
            Instantiate(ExplosionPrefab, transform.position, Quaternion.Inverse(ExplosionPrefab.transform.rotation));
            Destroy(gameObject);
        }

    }
}
