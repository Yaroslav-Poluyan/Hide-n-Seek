using UnityEngine;
using System.Collections.Generic;
using System.IO;
using _CodeBase.Services.LevelsData;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Data/JSON Generator", fileName = "JSONGenerator")]
public class JSONGenerator : ScriptableObject
{
    public List<LevelData> levelDatas = new();

    [Button]
    public void GenerateJson()
    {
        string jsonString = JsonUtility.ToJson(levelDatas.ToArray(), true);
        string filePath = Path.Combine(Application.dataPath, "levels.json");
        File.WriteAllText(filePath, jsonString);

        Debug.Log("JSON file generated: " + filePath);
    }
}