using System;
using Plugins.ReflexityAI.Framework;
using UnityEngine;

namespace AI {
    [Serializable, CreateAssetMenu(fileName = "TankAIBrain", menuName = "TankAIBrain")]
    public class TankAIBrain : AIBrainGraph<TankAI> {}
}