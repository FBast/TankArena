using System.Collections.Generic;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using SceneManager = Managers.SceneManager;

namespace UI {
    public class TeamFightUI : MonoBehaviour {

        [Header("Team A References")] 
        public Toggle TeamACompositeToggle;
        public List<TMP_Dropdown> TeamADropdowns;
        
        [Header("Team B References")] 
        public Toggle TeamBCompositeToggle;
        public List<TMP_Dropdown> TeamBDropdowns;
        
        [Header("Team C References")] 
        public Toggle TeamCCompositeToggle;
        public List<TMP_Dropdown> TeamCDropdowns;
        
        [Header("Team D References")] 
        public Toggle TeamDCompositeToggle;
        public List<TMP_Dropdown> TeamDDropdowns;

        private List<TankSetting> _tankSettings = new List<TankSetting>();
        
        private void Start() {
//            _settings = DataHandler.GetTankData();
            _tankSettings = SceneManager.Instance.TankSettings;
            InitDropDowns(TeamADropdowns, TeamACompositeToggle);
            InitDropDowns(TeamBDropdowns, TeamBCompositeToggle);
            InitDropDowns(TeamCDropdowns, TeamCCompositeToggle);
            InitDropDowns(TeamDDropdowns, TeamDCompositeToggle);
            SceneManager.Instance.SetParam(Properties.Parameters.GameType, Properties.GameTypes.TeamFight);
        }

        public void InitDropDowns(List<TMP_Dropdown> dropdowns, Toggle toggle) {
            foreach (TMP_Dropdown playerDropdown in dropdowns) {
                playerDropdown.ClearOptions();
                playerDropdown.AddOptions(new List<string> {"Empty"});
                playerDropdown.AddOptions(_tankSettings.Select(setting => setting.TankName + " of " + setting.PlayerName).ToList());
                playerDropdown.onValueChanged.AddListener(delegate(int value) {
                    if (!toggle.isOn) {
                        foreach (TMP_Dropdown dropdown in dropdowns) {
                            dropdown.SetValueWithoutNotify(value);
                        }
                    }
                });
                toggle.onValueChanged.AddListener(delegate(bool isComposite) {
                    for (int i = 1; i < dropdowns.Count; i++) {
                        dropdowns[i].interactable = isComposite;
                    }
                });
            }
        }

        public void LaunchGame() {
            SceneManager.Instance.SetParam(Properties.Parameters.TeamASettings, 
                (from playerDropdown in TeamADropdowns 
                where playerDropdown.value != 0 
                select _tankSettings[playerDropdown.value - 1]).ToList());
            SceneManager.Instance.SetParam(Properties.Parameters.TeamBSettings, 
                (from playerDropdown in TeamBDropdowns 
                    where playerDropdown.value != 0 
                    select _tankSettings[playerDropdown.value - 1]).ToList());
            SceneManager.Instance.SetParam(Properties.Parameters.TeamCSettings, 
                (from playerDropdown in TeamCDropdowns 
                    where playerDropdown.value != 0 
                    select _tankSettings[playerDropdown.value - 1]).ToList());
            SceneManager.Instance.SetParam(Properties.Parameters.TeamDSettings, 
                (from playerDropdown in TeamDDropdowns 
                    where playerDropdown.value != 0 
                    select _tankSettings[playerDropdown.value - 1]).ToList());
            SceneManager.Instance.UnloadScene(Properties.Scenes.Menu);
            SceneManager.Instance.LoadScene(Properties.Scenes.Game);
        }
        
    }
}
