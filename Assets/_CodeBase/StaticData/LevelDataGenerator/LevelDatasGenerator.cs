using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using _CodeBase.Services.LevelsData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using SerializationUtility = Sirenix.Serialization.SerializationUtility;

[CreateAssetMenu(menuName = "Level Data/JSON Generator", fileName = "JSONGenerator")]
public class LevelDatasGenerator : SerializedScriptableObject
{
    [SerializeField] public List<LevelData> levelDatas = new()
    {
        new LevelData
            { Id = 1, imageUrl = "https://picsum.photos/seed/1/1920/1080", imageName = "Level 1", counter = 15 },
        new LevelData
            { Id = 2, imageUrl = "https://picsum.photos/seed/2/1920/1080", imageName = "Level 2", counter = 20 },
        new LevelData
            { Id = 3, imageUrl = "https://picsum.photos/seed/3/1920/1080", imageName = "Level 3", counter = 18 },
        new LevelData
            { Id = 4, imageUrl = "https://picsum.photos/seed/4/1920/1080", imageName = "Level 4", counter = 22 },
        new LevelData
            { Id = 5, imageUrl = "https://picsum.photos/seed/5/1920/1080", imageName = "Level 5", counter = 25 },
        new LevelData
            { Id = 6, imageUrl = "https://picsum.photos/seed/6/1920/1080", imageName = "Level 6", counter = 17 },
        new LevelData
            { Id = 7, imageUrl = "https://picsum.photos/seed/7/1920/1080", imageName = "Level 7", counter = 19 },
        new LevelData
            { Id = 8, imageUrl = "https://picsum.photos/seed/8/1920/1080", imageName = "Level 8", counter = 23 },
        new LevelData
            { Id = 9, imageUrl = "https://picsum.photos/seed/9/1920/1080", imageName = "Level 9", counter = 21 },
        new LevelData
            { Id = 10, imageUrl = "https://picsum.photos/seed/10/1920/1080", imageName = "Level 10", counter = 27 }
    };

    [Button]
    public void GenerateJson()
    {
        string jsonString = OdinSerialize();
        string scriptableObjectPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
        if (scriptableObjectPath != null)
        {
            string filePath = Path.Combine(scriptableObjectPath, "levels.json");
            File.WriteAllText(filePath, jsonString);

            Debug.Log("JSON file generated: " + filePath);
        }
    }

    private string OdinSerialize()
    {
        var serializedData = SerializationUtility.SerializeValue(levelDatas, DataFormat.JSON);
        return Encoding.UTF8.GetString(serializedData);
    }
}