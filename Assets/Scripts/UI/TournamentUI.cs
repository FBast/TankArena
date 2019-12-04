using System.Collections.Generic;
using System.Linq;
using Data;
using Framework;
using Managers;
using SOReferences.GameReference;
using SOReferences.MatchReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TournamentUI : MonoBehaviour {

        [Header("Prefabs")] 
        public GameObject TeamTogglePrefab;
        public Sprite ColorSprite;

        [Header("Internal References")] 
        public Transform TeamButtonContent;
        public Transform TeamSettings;
        public List<TMP_Dropdown> TankSettingsDropdowns;
        public ColorPicker ColorPicker;

        [Header("SO References")] 
        public GameReference CurrentGameReference;
        public MatchReference CurrentMatchReference;
    
        private List<Team> _teams = new List<Team>();
        private Dictionary<Team, GameObject> _teamToggles = new Dictionary<Team, GameObject>();
        private Team _currentTeam;
        private List<TankSetting> _tankSettings = new List<TankSetting>();

        private void Start() {
            _tankSettings = Manager.Instance.TankSettings;
        }

        public void AddTeam() {
            Team team = new Team();
            _teams.Add(team);
            GameObject instantiate = Instantiate(TeamTogglePrefab, TeamButtonContent);
            Toggle toggle = instantiate.GetComponent<Toggle>();
            toggle.SetIsOnWithoutNotify(true);
            toggle.group = TeamButtonContent.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate(bool isToggle) {
                if (isToggle) {
                    _currentTeam = team;
                    UpdateTeamComposition();
                }
            });
            _currentTeam = team;
            _teamToggles.Add(team, instantiate);
            UpdateTeamComposition();
            TeamSettings.gameObject.SetActive(true);
        }

        public void RemoveTeam() {
            _teams.Remove(_currentTeam);
            Destroy(_teamToggles[_currentTeam]);
            _teamToggles.Remove(_currentTeam);
            if (_teams.Count > 0) {
                _currentTeam = _teams[0];
                _teamToggles[_currentTeam].GetComponent<Toggle>().SetIsOnWithoutNotify(true);
                UpdateTeamComposition();
            }
            else {
                TeamSettings.gameObject.SetActive(false);
            }
        }

        private void UpdateTeamComposition() {
            for (int i = 0; i < TankSettingsDropdowns.Count; i++) {
                TankSettingsDropdowns[i].ClearOptions();
                TankSettingsDropdowns[i].AddOptions(new List<string> {"Empty"});
                TankSettingsDropdowns[i].AddOptions(_tankSettings
                    .Select(setting => setting.TankName + " of " + setting.PlayerName).ToList());
                if (i < _currentTeam.TankSettings.Count) {
                    TankSettingsDropdowns[i].SetValueWithoutNotify(_tankSettings.IndexOf(_currentTeam.TankSettings[i]) + 1);
                }
                else {
                    TankSettingsDropdowns[i].SetValueWithoutNotify(0);
                }
                TankSettingsDropdowns[i].onValueChanged.AddListener(delegate {
                    _currentTeam.TankSettings.Clear();
                    foreach (TMP_Dropdown tmpDropdown in TankSettingsDropdowns) {
                        if (tmpDropdown.value > 0) _currentTeam.TankSettings.Add(_tankSettings[tmpDropdown.value - 1]);
                    }
                    _teamToggles[_currentTeam].GetComponentInChildren<TextMeshProUGUI>().text = _currentTeam.TeamName;
                    TeamSettings.gameObject.SetActive(true);
                });
            }
            ColorPicker.AssignColor(_currentTeam.Color);
            ColorPicker.onValueChanged.AddListener(delegate(Color color) { _currentTeam.Color = color; });
        }
    
        public void CreateGame() {
            Game game = new Game {
                Teams = _teams
            };
            game.SetupTournament();
            game.NextMatch();
            CurrentGameReference.Value = game;
            CurrentMatchReference.Value = CurrentGameReference.Value.CurrentMatch;
        }

    }
}
