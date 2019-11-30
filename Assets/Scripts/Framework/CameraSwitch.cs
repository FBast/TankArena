using Entities;
using SOReferences.GameObjectListReference;
using UnityEngine;
using Utils;

namespace Framework {
    public class CameraSwitch : MonoBehaviour {
    
        [Header("SO References")] 
        public GameObjectListReference TanksReference;
    
        private GameObject _tankCamera;
        private int _tankCameraIndex;
    
        private void Update() {
            if (Input.GetButtonDown(Properties.Inputs.LeftClick)) {
                _tankCameraIndex++;
                if (_tankCameraIndex >= TanksReference.Value.Count) _tankCameraIndex = 0;
                Switch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown(Properties.Inputs.RightClick)) {
                _tankCameraIndex--;
                if (_tankCameraIndex < 0) _tankCameraIndex = TanksReference.Value.Count - 1;
                Switch(TanksReference.Value[_tankCameraIndex].GetComponent<TankEntity>().TurretCamera);
            }
            if (Input.GetButtonDown(Properties.Inputs.WheelClick)) {
                _tankCamera.SetActive(false);
            }
        }

        private void Switch(GameObject newCamera) {
            if (_tankCamera)
                _tankCamera.SetActive(false);
            _tankCamera = newCamera;
            newCamera.SetActive(true);
        }
    
    }
}
