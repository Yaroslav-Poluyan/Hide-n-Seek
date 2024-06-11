using System;
using System.Collections;
using _CodeBase.Services.Curtain;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoaderReferencesSO _sceneLoaderReferencesSO;
        private readonly ISceneLoadingCurtain _sceneLoadingCurtain;
        public SceneType CurrentSceneType { get; set; }
        public Action<SceneType> OnSceneChanged { get; set; }

        public SceneLoader(ICoroutineRunner coroutineRunner, SceneLoaderReferencesSO sceneLoaderReferencesSO,
            ISceneLoadingCurtain sceneLoadingCurtain)
        {
            _coroutineRunner = coroutineRunner;
            _sceneLoaderReferencesSO = sceneLoaderReferencesSO;
            _sceneLoadingCurtain = sceneLoadingCurtain;
        }

        public void Load(SceneType type, Action onLoaded = null, Action allowSceneActivation = null)
        {
            if (type == SceneType.None)
            {
                throw new ArgumentException("SceneType.None is not allowed");
            }

            if (!_sceneLoaderReferencesSO.Scenes.TryGetValue(type, out var scenePath))
            {
                throw new ArgumentException($"SceneType {type} is not registered in {nameof(SceneLoaderReferencesSO)}");
            }

            Load(scenePath, onLoaded, allowSceneActivation);
        }

        private void Load(string name, Action onLoaded = null, Action allowSceneActivation = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, allowSceneActivation));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null, Action allowSceneActivation = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);


            if (allowSceneActivation != null)
            {
                waitNextScene.allowSceneActivation = false;
                allowSceneActivation += () => waitNextScene.allowSceneActivation = true;
            }

            while (!waitNextScene.isDone)
            {
#if UNITY_EDITOR
                //Debug.Log("<color=green>Loading progress: " + waitNextScene.progress + "</color>");
#endif
                _sceneLoadingCurtain.SetProgress(waitNextScene.progress);
                yield return null;
            }
            onLoaded?.Invoke();
            CurrentSceneType = _sceneLoaderReferencesSO.GetSceneType(nextScene);
            OnSceneChanged?.Invoke(CurrentSceneType);
        }
    }
}