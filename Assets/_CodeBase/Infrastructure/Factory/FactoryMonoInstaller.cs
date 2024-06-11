using _CodeBase.Infrastructure.Factory.Game;
using Zenject;

namespace _CodeBase.Infrastructure.Factory
{
    public class FactoryMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle().NonLazy();
        }
    }
}