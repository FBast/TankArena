using System.Collections.Generic;
using System.Linq;
using Data;
using Framework;
using SOReferences.GameReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using SceneManager = Managers.SceneManager;

namespace UI {
    public class TeamFightUI : MonoBehaviour {

        [Header("SO References")]
        public GameReference CurrentGameReference;
        
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

        private void InitDropDowns(List<TMP_Dropdown> dropdowns, Toggle toggle) {
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
            Game game = new Game();
            if (TeamADropdowns.Sum(dropdown => dropdown.value) > 0) {
                game.Teams.Add(new Team
                {
                    TankSettings = (from playerDropdown in TeamADropdowns 
                        where playerDropdown.value != 0 
                        select _tankSettings[playerDropdown.value - 1]).ToList(),
                    Color = Color.red
                });
            }
            if (TeamBDropdowns.Sum(dropdown => dropdown.value) > 0) {
                game.Teams.Add(new Team {
                    TankSettings = (from playerDropdown in TeamBDropdowns
                        where playerDropdown.value != 0
                        select _tankSettings[playerDropdown.value - 1]).ToList(),
                    Color = Color.green
                });
            }
            if (TeamCDropdowns.Sum(dropdown => dropdown.value) > 0) {
                game.Teams.Add(new Team {
                    TankSettings = (from playerDropdown in TeamCDropdowns
                        where playerDropdown.value != 0
                        select _tankSettings[playerDropdown.value - 1]).ToList(),
                    Color = Color.blue
                });
            }
            if (TeamDDropdowns.Sum(dropdown => dropdown.value) > 0) {
                game.Teams.Add(new Team {
                    TankSettings = (from playerDropdown in TeamDDropdowns
                        where playerDropdown.value != 0
                        select _tankSettings[playerDropdown.value - 1]).ToList(),
                    Color = Color.yellow
                });
            }
            game.SetupTeamFight();
            CurrentGameReference.Value = game;
            SceneManager.Instance.UnloadScene(Properties.Scenes.Menu);
            SceneManager.Instance.LoadScene(Properties.Scenes.Game);
        }
        
    }
}
