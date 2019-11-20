using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers {
	public class SceneManager : Singleton<SceneManager> {

		private void Start() {
			LoadScene(Properties.Scenes.Menu);
		}

		private Dictionary<string, object> _parameters;
		
		public void UnloadScene(string scene) {
			if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene).isLoaded) return;
			ConsoleProDebug.LogToFilter("Unloading " + scene + "...", "Management");
			StartCoroutine(UnloadSceneAsync(scene));
		}
		
		public void LoadScene(string scene) {
			if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene).isLoaded) return;
			ConsoleProDebug.LogToFilter("Loading " + scene + "...", "Management");
			StartCoroutine(LoadSceneAsync(scene));
		}

		public void ReloadScene(string scene) {
			ConsoleProDebug.LogToFilter("Reloading " + scene + "...", "Management");
			StartCoroutine(ReloadSceneAsync(scene));
		}

		public IEnumerator ReloadSceneAsync(string scene) {
			yield return UnloadSceneAsync(scene);
			yield return LoadSceneAsync(scene);
		}
		
		private IEnumerator UnloadSceneAsync(string scene) {
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
			//Wait until the last operation fully loads to return anything
			while (!asyncLoad.isDone) {
				yield return null;
			}
			ConsoleProDebug.LogToFilter(scene + " unloaded !", "Management");
		}

		private IEnumerator LoadSceneAsync(string scene) {
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
			//Wait until the last operation fully loads to return anything
			while (!asyncLoad.isDone) {
				yield return null;
			}
			ConsoleProDebug.LogToFilter(scene + " loaded !", "Management");
		}
		
		public object GetParam(string paramKey) {
			if (_parameters == null) return null;
			return _parameters.ContainsKey(paramKey) ? _parameters[paramKey] : null;
		}
 
		public void SetParam(string paramKey, object paramValue) {
			if (_parameters == null)
				_parameters = new Dictionary<string, object>();
			RemoveParam(paramKey);
			_parameters.Add(paramKey, paramValue);
		}

		public void RemoveParam(string paramKey) {
			if (_parameters.ContainsKey(paramKey))
				_parameters.Remove(paramKey);
		}

	}
}