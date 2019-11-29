using Entities;
using UnityEngine;
using Utils;

public class ExplosionDamage : MonoBehaviour {

    public int Radius;
    
    private readonly Collider[] _hitColliders = new Collider[10];

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
    
    private void OnDestroy() {
        int size = Physics.OverlapSphereNonAlloc(transform.position, Radius, _hitColliders);
        for (int i = 0; i < size; i++) {
            TankEntity tankEntity = _hitColliders[i].GetComponent<TankEntity>();
            if (tankEntity) 
                tankEntity.Damage(PlayerPrefs.GetInt(Properties.PlayerPrefs.ExplosionDamage, 
                    Properties.PlayerPrefsDefault.ExplosionDamage));
        }
    }

}
