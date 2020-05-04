using System.Collections.Generic;
using System.Linq;
using Entities;
using Plugins.ReflexityAI.Framework;
using UnityEngine;

namespace AI {
    public class TankAI : ReflexityAI {

        // Your custom references here
        public List<BonusEntity> BonusEntities => GetComponent<TankEntity>().BonusReference.Value
            .Select(o => o.GetComponent<BonusEntity>()).ToList();
        public List<TankEntity> TankEntities => GetComponent<TankEntity>().TanksReference.Value
            .Select(o => o.GetComponent<TankEntity>()).ToList();
        public List<WaypointEntity> WaypointEntities => GetComponent<TankEntity>().WaypointsReference.Value
            .Select(o => o.GetComponent<WaypointEntity>()).ToList();
        [HideInInspector] public TankEntity TankEntity;
        // End of custom references

        private void Awake() {
            TankEntity = GetComponent<TankEntity>();
        }

    }
}