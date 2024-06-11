using System.Threading.Tasks;
using _CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Factory
{
    public abstract class FactoryBase
    {
        protected readonly DiContainer container;
        protected readonly IAssetProvider assetProvider;

        protected FactoryBase(DiContainer container, IAssetProvider assetProvider)
        {
            this.container = container;
            this.assetProvider = assetProvider;
        }

        protected async Task<TValue> CreateAndInject<TValue>(string assetPath, Vector3 position, Quaternion rotation,
            Transform parent = null) where TValue : Object
        {
            var prefab = await Load<TValue>(assetPath);
            return CreateAndInject(prefab, position, rotation, parent);
        }

        protected TValue CreateAndInject<TValue>(TValue prefab, Vector3 position, Quaternion rotation,
            Transform parent = null) where TValue : Object
        {
            TValue obj;
            if (typeof(TValue) == typeof(GameObject))
            {
                obj = container.InstantiatePrefab(prefab, position, rotation, parent) as TValue;
            }
            else if (typeof(TValue) == typeof(Component))
            {
                obj = container.InstantiatePrefabForComponent<TValue>(prefab, position, rotation,
                    parent);
            }
            else
            {
                obj = container.InstantiatePrefabForComponent<TValue>(prefab, position, rotation,
                    parent);
            }

            return obj;
        }

        protected async Task<TValue> Create<TValue>(string assetPath, Vector3 position, Quaternion rotation,
            Transform parent) where TValue : Object
        {
            var asset = await Load<TValue>(assetPath);
            return Create(asset, position, rotation, parent);
        }

        private TValue Create<TValue>(TValue prefab, Vector3 position, Quaternion rotation,
            Transform parent) where TValue : Object
        {
            TValue obj;
            if (typeof(TValue) == typeof(GameObject))
            {
                obj = Object.Instantiate(prefab, position, rotation, parent);
            }
            else if (typeof(TValue) == typeof(Component))
            {
                obj = Object.Instantiate(prefab, position, rotation, parent);
            }
            else
            {
                obj = Object.Instantiate(prefab, position, rotation, parent);
            }

            return obj;
        }

        private async Task<TValue> Load<TValue>(string assetPath) where TValue : Object
        {
            var cancellationToken = new System.Threading.CancellationToken();
            return await assetProvider.LoadAs<TValue>(assetPath, cancellationToken);
        }
    }
}