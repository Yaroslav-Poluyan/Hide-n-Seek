using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace _CodeBase.StaticData.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<Type, object> _staticDataMap;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _staticDataMap = new Dictionary<Type, object>();
        }

        public Dictionary<Type, string> AssetPathsHashmap => new()
        {
        };

        public async Task Load(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>
            {
            };
            await Task.WhenAll(tasks);
        }

        public async Task LoadStaticData<T>() where T : ScriptableObject, IStaticData
        {
            if (_staticDataMap.ContainsKey(typeof(T))) return;
            var cancellationToken = new CancellationToken();
            _staticDataMap[typeof(T)] = await _assetProvider.LoadAs<T>(AssetPathsHashmap[typeof(T)], cancellationToken);
            if (_staticDataMap[typeof(T)] == null)
            {
                Debug.LogError($"Static data {typeof(T)} is null");
            }
        }

        public T GetStaticData<T>() where T : IStaticData
        {
            if (_staticDataMap.TryGetValue(typeof(T), out var data))
            {
                return (T)data;
            }

            Debug.LogError($"No static data found for type {typeof(T)}");
            return default;
        }
    }
}