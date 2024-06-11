using System.Threading.Tasks;
using UnityEngine;

namespace _CodeBase.Services.WebRequests
{
    public interface IImageDownloaderService
    {
        public Task<Sprite> GetSprite(string url);
        public Task<bool> CheckAccessToUrl(string url);
    }
}