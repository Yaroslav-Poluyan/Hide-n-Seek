using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _CodeBase.Infrastructure.AssetManagement
{
   public interface IAssetProvider
   {
      Task<GameObject> Load(string assetPath);
      Task<TValue> LoadAs<TValue>(string assetPath, CancellationToken cancellationToken) where TValue : Object;
      public Task<TValue[]> LoadAll<TValue>(string assetsPath);
   }
}