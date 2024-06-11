using _CodeBase.Infrastructure.AssetManagement;
using Zenject;

namespace _CodeBase.Infrastructure.Factory.UIFactory
{
    public class UIFactory : FactoryBase, IUIFactory
    {
        public UIFactory(DiContainer container, IAssetProvider assetProvider) :
            base(container, assetProvider)
        {
        }
    }
}