using UnityEditor;
using UnityEngine;

namespace Entities.Editor {
    [CustomEditor(typeof(TankSetting))]
    public class TankDataEditor : UnityEditor.Editor {
        
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            TankSetting tankSetting = (TankSetting) target;
            if(GUILayout.Button("Save")) {
                tankSetting.Save();
            }
        }
    }
}