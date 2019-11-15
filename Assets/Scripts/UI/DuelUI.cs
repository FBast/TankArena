using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class DuelUI : MonoBehaviour {

        [Header("References")] 
        public TextMeshProUGUI NumberOfTankText;
        public Dropdown FirstPlayerDropdown;
        public Dropdown SecondPlayerDropdown;

        public List<TankSetting> Settings = new List<TankSetting>();
        
        private void Start() {
            foreach (string directory in Directory.GetDirectories(Application.streamingAssetsPath)) {
                print(directory);
            }
            DirectoryInfo info = new DirectoryInfo(Application.streamingAssetsPath);
            FileInfo[] fileInfos = info.GetFiles();
            foreach (FileInfo fileInfo in fileInfos) {
                print(fileInfo);
            }
//            FirstPlayerDropdown.AddOptions(new List<string> {
                
//            });
        }

        public void SetNumberOfTankPerPlayer(float value) {
            NumberOfTankText.text = value.ToString(CultureInfo.CurrentCulture);
        }
        
    }
}
