using UnityEditor;
using UnityEngine;

namespace Plugins.NodeUtilityAi.Editor {
    public class AiDebuggerEditor : EditorWindow {

        float armor = 20;

        [MenuItem("Tool/AI Debugger")]
        private static void Init() {
            AiDebuggerEditor window = GetWindow<AiDebuggerEditor>("AI Debugger", true);
            window.Show();
        }

        private void OnGUI() {
            int columnNumber = 2;
            int rowNumber = 5;
            float columnWidth = (position.width - 6) / columnNumber;
            float rowWidth = 20;
            for (int i = 0; i < columnNumber; i++) {
                for (int j = 0; j < rowNumber; j++) {
                    EditorGUI.ProgressBar(new Rect(3 + i * columnWidth, 3 + j * rowWidth, columnWidth, rowWidth), armor / 100, "Armor");
                }
            }

        }
        
    }
}
