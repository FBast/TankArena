using Framework;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI {
    public class SettingsUI : MonoBehaviour {

        [Header("Internal References")] 
        public SliderWithLabelUI MatchDuration;
        public SliderWithLabelUI HealthPoints;
        public SliderWithLabelUI CanonDamage;
        public SliderWithLabelUI CanonPower;
        public SliderWithLabelUI TurretSpeed;
        public SliderWithLabelUI ReloadTime;
        public SliderWithLabelUI WaypointSeekRadius;
        public SliderWithLabelUI ExplosionDamage;
        public SliderWithLabelUI ExplosionRadius;
        public Toggle ExplosionCreateBustedTank;
        public SliderWithLabelUI SecondsBetweenRefresh;
        public Toggle AlwaysPickBestChoice;

        private void Start() {
            MatchDuration.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.MatchDuration, (int) value);
            });
            HealthPoints.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.HealthPoints, (int) value);
            });
            CanonDamage.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.CanonDamage, (int) value);
            });
            CanonPower.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.CanonPower, (int) value);
            });
            TurretSpeed.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.TurretSpeed, (int) value);
            });
            ReloadTime.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.ReloadTime, (int) value);
            });
            WaypointSeekRadius.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.WaypointSeekRadius, (int) value);
            });
            ExplosionDamage.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.ExplosionDamage, (int) value);
            });
            ExplosionRadius.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.ExplosionRadius, (int) value);
            });
            ExplosionCreateBustedTank.onValueChanged.AddListener(delegate(bool value) {
                PlayerPrefsUtils.SetBool(Properties.PlayerPrefs.ExplosionCreateBustedTank, value);
            });
            SecondsBetweenRefresh.OnValueChanged.AddListener(delegate(float value) {
                PlayerPrefs.SetInt(Properties.PlayerPrefs.SecondsBetweenRefresh, (int) value);
            });
            AlwaysPickBestChoice.onValueChanged.AddListener(delegate(bool value) {
                PlayerPrefsUtils.SetBool(Properties.PlayerPrefs.AlwaysPickBestChoice, value);
            });
            UpdateSettings();
        }

        private void UpdateSettings() {
            MatchDuration.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.MatchDuration,
                Properties.PlayerPrefsDefault.MatchDuration);
            HealthPoints.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.HealthPoints,
                Properties.PlayerPrefsDefault.HealthPoints);
            CanonDamage.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.CanonDamage,
                Properties.PlayerPrefsDefault.CanonDamage);
            CanonPower.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.CanonPower,
                Properties.PlayerPrefsDefault.CanonPower);
            TurretSpeed.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.TurretSpeed,
                Properties.PlayerPrefsDefault.TurretSpeed);
            ReloadTime.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.ReloadTime,
                Properties.PlayerPrefsDefault.ReloadTime);
            WaypointSeekRadius.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.WaypointSeekRadius,
                Properties.PlayerPrefsDefault.WaypointSeekRadius);
            ExplosionDamage.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.ExplosionDamage,
                Properties.PlayerPrefsDefault.ExplosionDamage);
            ExplosionRadius.Value = PlayerPrefs.GetInt(Properties.PlayerPrefs.ExplosionRadius,
                Properties.PlayerPrefsDefault.ExplosionRadius);
            ExplosionCreateBustedTank.isOn = PlayerPrefsUtils.GetBool(Properties.PlayerPrefs.ExplosionCreateBustedTank,
                Properties.PlayerPrefsDefault.ExplosionCreateBustedTank);
            SecondsBetweenRefresh.Value = PlayerPrefs.GetFloat(Properties.PlayerPrefs.SecondsBetweenRefresh,
                Properties.PlayerPrefsDefault.SecondsBetweenRefresh);
            AlwaysPickBestChoice.isOn = PlayerPrefsUtils.GetBool(Properties.PlayerPrefs.AlwaysPickBestChoice,
                Properties.PlayerPrefsDefault.AlwaysPickBestChoice);
        }
        
        public void ResetDefault() {
            PlayerPrefs.DeleteAll();
            UpdateSettings();
        }
        
    }
}