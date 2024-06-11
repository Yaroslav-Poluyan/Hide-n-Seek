using System.Collections.Generic;
using System.Threading.Tasks;
using _CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace _CodeBase.Services.LevelsData
{
    internal class LevelsDataService : ILevelsDataService
    {
        public List<LevelData> levels = new List<LevelData>();

        public async Task LoadLevelsDataAsync()
        {
            try
            {
                var jsonString = await LoadTextFromUrlAsync(AssetsPaths.LevelsDataURL);
                ParseLevelsFromJson(jsonString);
                await LoadLevelImagesAsync();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error loading levels: " + ex.Message);
            }
        }

        void ParseLevelsFromJson(string jsonString)
        {
            levels = new List<LevelData>(JsonUtility.FromJson<LevelData[]>(jsonString));
        }

        async Task LoadLevelImagesAsync()
        {
            foreach (var level in levels)
            {
                try
                {
                    var texture = await LoadTextureFromUrlAsync(level.imageUrl);
                    //todo: dont forget to assign texture to level object
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error loading level image: " + ex.Message);
                }
            }
        }

        async Task<string> LoadTextFromUrlAsync(string url)
        {
            using var request = UnityWebRequest.Get(url);
            request.SendWebRequest();
            while (!request.isDone) await Task.Yield();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
                or UnityWebRequest.Result.DataProcessingError)
            {
                throw new System.Exception(request.error);
            }

            return request.downloadHandler.text;
        }

        async Task<Texture2D> LoadTextureFromUrlAsync(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            request.SendWebRequest();
            while (!request.isDone) await Task.Yield();

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
                or UnityWebRequest.Result.DataProcessingError)
            {
                throw new System.Exception(request.error);
            }

            return ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        public List<LevelData> GetLevelDatas()
        {
            return levels;
        }
    }
}