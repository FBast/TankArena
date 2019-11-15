using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Components {
    public class Spinning : MonoBehaviour {

        public Vector3 Speed;
        
        private void Update() {
            transform.Rotate(Speed * Time.deltaTime);
        }
        
    }
}
