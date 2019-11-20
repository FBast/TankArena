using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Entities;
using TMPro;
using UnityEngine;
using Utils;
using SceneManager = Managers.SceneManager;

namespace UI {
    public class DuelUI : MonoBehaviour {

        [Header("References")] 
        public TextMeshProUGUI NumberOfTankText;
        public TMP_Dropdown FirstPlayerDropdown;
        public TMP_Dropdown SecondPlayerDropdown;

        private List<TankSetting> _settings = new List<TankSetting>();
        private TankSetting _firstPlayerTankSetting;
        private TankSetting _secondPlayerTankSetting;
        private int _tankNumber;
        
        private void Start() {
            _settings = DataHandler.GetTankData();
            FirstPlayerDropdown.ClearOptions();
            FirstPlayerDropdown.AddOptions(_settings.Select(setting => setting.TankName + " of " + setting.PlayerName).ToList());
            FirstPlayerDropdown.onValueChanged.AddListener(SetFirstPlayer);
            SetFirstPlayer(0);
            SecondPlayerDropdown.ClearOptions();
            SecondPlayerDropdown.AddOptions(_settings.Select(setting => setting.TankName + " of " + setting.PlayerName).ToList());
            SecondPlayerDropdown.onValueChanged.AddListener(SetSecondPlayer);
            SetSecondPlayer(0);
            SceneManager.Instance.SetParam(Properties.Parameters.GameType, Properties.GameTypes.Duel);
        }

        private void SetSecondPlayer(int index) {
            _secondPlayerTankSetting = _settings[index];
        }

        private void SetFirstPlayer(int index) {
            _firstPlayerTankSetting = _settings[index];
        }

        public void SetNumberOfTankPerPlayer(float value) {
            NumberOfTankText.text = value.ToString(CultureInfo.CurrentCulture);
            _tankNumber = (int) value;
        }

        public void LaunchGame() {
            SceneManager.Instance.SetParam(Properties.Parameters.TankSettings, new List<TankSetting> {
                _firstPlayerTankSetting, _secondPlayerTankSetting
            });
            SceneManager.Instance.SetParam(Properties.Parameters.TankNumber, _tankNumber);
            SceneManager.Instance.UnloadScene(Properties.Scenes.Menu);
            SceneManager.Instance.LoadScene(Properties.Scenes.Game);
        }
        
    }
}
