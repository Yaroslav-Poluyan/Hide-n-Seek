using _CodeBase.Infrastructure.AssetManagement;
using Zenject;

namespace _CodeBase.Infrastructure.Factory.Game
{
    public class GameFactory : FactoryBase, IGameFactory
    {
        public GameFactory(DiContainer container, IAssetProvider assetProvider)
            : base(container, assetProvider)
        {
        }
    }
}