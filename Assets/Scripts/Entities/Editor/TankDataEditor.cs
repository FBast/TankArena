using UnityEditor;
using UnityEngine;

namespace Entities.Editor {
    [CustomEditor(typeof(TankData))]
    public class TankDataEditor : UnityEditor.Editor {
        
        public override async void OnInspectorGUI() {
            DrawDefaultInspector();
            TankData tankData = (TankData) target;
            if(GUILayout.Button("Save")) {
                await tankData.Save();
            }
        }
    }
}