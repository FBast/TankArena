using System;
using UnityEngine;

namespace Components {
    public class FreeLookCamera : MonoBehaviour {

        [Header("Movement")]
        public string LeftRightMovementAxis;
        public string ForwardBackwardMovementAxis;
        public string UpDownMovementAxis;
        public int MovementSpeed;

        [Header("Rotation")]
        public string YawRotationAxis;
        public string PitchRotationAxis;
        public string RollRotatationAxis;
        public int RotationSpeed;
        
        private void FixedUpdate() {
            float totalMovementSpeed = Time.fixedDeltaTime * MovementSpeed;
            transform.Translate(Input.GetAxis(LeftRightMovementAxis) * totalMovementSpeed, Input.GetAxis(UpDownMovementAxis) * totalMovementSpeed, Input.GetAxis(ForwardBackwardMovementAxis) * totalMovementSpeed);
            float totalRotationSpeed = Time.fixedDeltaTime * RotationSpeed;
            transform.Rotate(-Input.GetAxis(PitchRotationAxis) * totalRotationSpeed, Input.GetAxis(YawRotationAxis) * totalRotationSpeed, -Input.GetAxis(RollRotatationAxis) * totalRotationSpeed);
        }
        
    }
}
