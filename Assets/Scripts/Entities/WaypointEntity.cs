using System.Linq;
using Framework;
using SOReferences.GameObjectListReference;
using UnityEngine;
using Utils;

namespace Entities {
    public class WaypointEntity : MonoBehaviour {

        [Header("Parameters")] 
        public LayerMask CoverLayer;
        
        [Header("SO References")]
        public GameObjectListReference TanksReference;
        
        public Transform Transform => transform;
        
        public int ObserverCount(TankEntity hider, FactionType seekersFaction) {
            if (seekersFaction == FactionType.All) {
                return transform.InLineOfView(transform,
                    TanksReference.Value
                        .Select(o => o.GetComponent<TankEntity>())
                        .Where(entity => entity != hider)
                        .Select(entity => entity.transform).ToList(), CoverLayer).Count;
            }
            return transform.InLineOfView(transform,
                TanksReference.Value
                    .Select(o => o.GetComponent<TankEntity>())
                    .Where(entity => entity != hider && hider.GetFaction(entity) == seekersFaction)
                    .Select(entity => entity.transform).ToList(), CoverLayer).Count;
        } 
        
    }
}
