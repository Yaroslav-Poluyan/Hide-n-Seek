﻿using System.Collections.Generic;
using System.Linq;
using _CodeBase.Technical;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.SceneLoading
{
    [CreateAssetMenu(fileName = "SceneReferencesSO", menuName = "Data/Scenes", order = 0)]
    public class SceneLoaderReferencesSO : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<SceneType, string> Scenes { get; private set; } = new();

        public SceneType GetSceneType(string sceneName)
        {
            return (from scene in Scenes where scene.Value == sceneName select scene.Key).FirstOrDefault();
        }
    }

    public enum SceneType
    {
        None = 0,
        Bootstrapper = 1,
        InitialLoading = 2,
        MainMenu = 3,
        Settings = 5,
        Controls = 6,
        Audio = 7,
    }

    public static class SceneTypeExtensions
    {
    }
}