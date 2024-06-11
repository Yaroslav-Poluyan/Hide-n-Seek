using System.Threading.Tasks;
using _CodeBase.Infrastructure.AssetManagement;
using _CodeBase.Infrastructure.GameplayLogic.MainMenu;
using _CodeBase.Services.LevelsData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Factory.UIFactory
{
    public class UIFactory : FactoryBase, IUIFactory
    {
        public UIFactory(DiContainer container, IAssetProvider assetProvider) :
            base(container, assetProvider)
        {
        }

        public async Task<Transform> CreateLevelRow(Transform rowsParent) =>
            await CreateAndInject<Transform>(AssetsPaths.UILevelRow, Vector3.zero,
                Quaternion.identity, rowsParent);

        public async Task<LevelBlock> CreateLevelBlock(Transform rowTransform) =>
            await CreateAndInject<LevelBlock>(AssetsPaths.UILevelBlock, Vector3.zero, Quaternion.identity,
                rowTransform);
    }
}