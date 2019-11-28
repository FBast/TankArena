using Framework;
using SOReferences.MatchReference;
using UnityEngine;

namespace Entities {
    public class ShellEntity : MonoBehaviour {

        [Header("Prefabs")]
        public GameObject ExplosionPrefab;

        [Header("SO References")] 
        public MatchReference CurrentMatchReference;
        
        [HideInInspector] public TankEntity TankEntityOwner;
        [HideInInspector] public int Damage;

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<ShellEntity>()) return;
            TankEntity tankEntity = other.GetComponent<TankEntity>();
            if (tankEntity == TankEntityOwner) return;
            if (tankEntity) {
                if (tankEntity.CurrentHP - Damage < 0)
                    CurrentMatchReference.Value.MatchStats[TankEntityOwner.Team].KillCount++;
                tankEntity.Damage(Damage);
                CurrentMatchReference.Value.MatchStats[TankEntityOwner.Team].DamageDone += Damage;
            }
            Instantiate(ExplosionPrefab, transform.position, Quaternion.Inverse(ExplosionPrefab.transform.rotation));
            Destroy(gameObject);
        }

    }
}
