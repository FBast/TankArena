using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Framework;
using SOEvents.StringEvents;
using SOReferences.GameReference;
using SOReferences.MatchReference;
using UnityEngine;

namespace UI {
    public class MatchEndUI : MonoBehaviour {

        [Header("Prefabs")] 
        public GameObject TeamStatLine;

        [Header("Internal References")] 
        public Transform StatsContent;
        
        [Header("SO References")] 
        public GameReference CurrentGameReference;
        public MatchReference CurrentMatchReference;

        [Header("SO Events")] 
        public StringEvent OnReloadScene;
        public StringEvent OnUnloadScene;
        public StringEvent OnLoadScene;

        private void OnEnable() {
            foreach (KeyValuePair<Team,Stats> teamStat in CurrentMatchReference.Value.TeamStats) {
                TeamStatLineUI teamStatLineUi = Instantiate(TeamStatLine, StatsContent.transform)
                    .GetComponent<TeamStatLineUI>();
                teamStatLineUi.TeamNameText.text = teamStat.Key.TeamName;
                teamStatLineUi.TankLeftText.text = teamStat.Value.TankLeft.ToString();
                teamStatLineUi.KillCountText.text = teamStat.Value.KillCount.ToString();
                teamStatLineUi.LossCountText.text = teamStat.Value.LossCount.ToString();
                teamStatLineUi.TeamKillText.text = teamStat.Value.TeamKill.ToString();
                teamStatLineUi.DamageDoneText.text = teamStat.Value.DamageDone.ToString();
                teamStatLineUi.DamageSufferedText.text = teamStat.Value.DamageSuffered.ToString();
                teamStatLineUi.TeamDamageText.text = teamStat.Value.TeamDamage.ToString();
            }
        }

        private void OnDisable() {
            foreach (Transform child in StatsContent.transform) {
                Destroy(child.gameObject);
            }
        }

        public void NextMatch() {
            if (CurrentGameReference.Value.NextMatch() != null) {
                CurrentMatchReference.Value = CurrentGameReference.Value.CurrentMatch;
                OnReloadScene.Raise(Properties.Scenes.Game);
            }
            else {
                OnUnloadScene.Raise(Properties.Scenes.Game);
                OnLoadScene.Raise(Properties.Scenes.Menu);
            }
        }

        public void RestartMatch() {
            CurrentMatchReference.Value.ResetStats();
            OnReloadScene.Raise(Properties.Scenes.Game);
        }
        
    }
}
