using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using SceneManager = Managers.SceneManager;

namespace UI {
    public class TeamFightUI : MonoBehaviour {

        [Header("References")] 
        public List<TMP_Dropdown> TeamADropdowns;
        public List<TMP_Dropdown> TeamBDropdowns;
        public List<TMP_Dropdown> TeamCDropdowns;
        public List<TMP_Dropdown> TeamDDropdowns;

        private List<TankSetting> _tankSettings = new List<TankSetting>();
        
        private void Start() {
//            _settings = DataHandler.GetTankData();
            _tankSettings = SceneManager.Instance.TankSettings;
            InitDropDowns(TeamADropdowns);
            InitDropDowns(TeamBDropdowns);
            InitDropDowns(TeamCDropdowns);
            InitDropDowns(TeamDDropdowns);
            SceneManager.Instance.SetParam(Properties.Parameters.GameType, Properties.GameTypes.TeamFight);
        }

        public void InitDropDowns(List<TMP_Dropdown> dropdowns) {
            foreach (TMP_Dropdown playerDropdown in dropdowns) {
                playerDropdown.ClearOptions();
                playerDropdown.AddOptions(new List<string> {"Empty"});
                playerDropdown.AddOptions(_tankSettings.Select(setting => setting.TankName + " of " + setting.PlayerName).ToList());
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
