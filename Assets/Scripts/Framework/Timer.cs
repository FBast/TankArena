using SOEvents.VoidEvents;
using UnityEngine;

namespace Framework {
    public class Timer : MonoBehaviour {

        [Header("SO Events")] 
        public VoidEvent OnTimerFinished;

        private float _timer;
        private float _currentTime;

        private void Start() {
            _timer = PlayerPrefs.GetInt(Properties.PlayerPrefs.MatchDuration,
                Properties.PlayerPrefsDefault.MatchDuration);
            Time.timeScale = 1;
        }

        private void Update() {
            _currentTime += Time.deltaTime;
            if (_currentTime < _timer) return;
            OnTimerFinished.Raise();
            Time.timeScale = 0;
        }

    }
}
