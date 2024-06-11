using System;

namespace _CodeBase.Infrastructure.SceneLoading
{
    public interface ISceneLoader
    {
        void Load(SceneType type, Action onLoaded = null, Action allowSceneActivation = null);
        SceneType CurrentSceneType { get; set; }
        Action<SceneType> OnSceneChanged { get; set; }
    }
}