using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _CodeBase.Services.WebRequests
{
    public class ImageDownloaderService : IImageDownloaderService
    {
        public async Task<Sprite> GetSprite(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Sprite sprite = Sprite.Create(texture, rect, pivot);
                return sprite;
            }

            Debug.LogError($"Failed to download image: {request.error}");
            return null;
        }

        public Task<bool> CheckAccessToUrl(string url)
        {
            using var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                return Task.FromResult(true);
            }

            Debug.LogError($"Failed to download image: {request.error}");
            return Task.FromResult(false);
        }
    }
}