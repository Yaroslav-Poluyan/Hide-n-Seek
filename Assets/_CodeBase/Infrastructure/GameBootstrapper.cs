using System.Threading.Tasks;
using _CodeBase.Infrastructure.AssetManagement;
using _CodeBase.Infrastructure.Factory.Game;
using _CodeBase.Infrastructure.Factory.InfrastructureFactories;
using _CodeBase.Infrastructure.Factory.UIFactory;
using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using _CodeBase.Services;
using _CodeBase.Services.Curtain;
using _CodeBase.Services.Input;
using _CodeBase.StaticData.StaticData;
using UnityEngine;
using Zenject;
using PlayerProgressData = _CodeBase.StaticData.PlayerProgressData.PlayerProgressData;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace _CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoInstaller, ICoroutineRunner
    {
        private Game _game;

        public override async void InstallBindings()
        {
            BindInfrastructureFactory();
            BindCoroutineRunner();
            BindAssetProvider();
            await BindStaticDataService();
            BindPlayerProgressData();
            await BindLoadingSceneCurtain();
            await BindSceneReferencesSO();
            BindSceneLoader();
            BindInputService();
            BindGameFactory();
            BindUIFactory();
            BindTimeManager();
            _game = new Game(Container.Resolve<IInfrastructureFactory>());
            BindGameStateMachine();
            BindGame();
            EnterToBootstrapState();
        }

        public override void Start()
        {
        }

        private void BindTimeManager()
        {
            var timeManagerService = Instantiate(new GameObject()).AddComponent<TimeManagerService>();
            timeManagerService.transform.parent = transform;
            timeManagerService.gameObject.name = "TimeManagerService";
            Container.Bind<TimeManagerService>().FromInstance(timeManagerService).AsSingle().NonLazy();
        }

        private void BindPlayerProgressData()
        {
            Container.Bind<PlayerProgressData>()
                .AsSingle()
                .NonLazy();
        }

        private void EnterToBootstrapState()
        {
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void BindInfrastructureFactory() => Container.Bind<IInfrastructureFactory>()
            .To<InfrastructureFactory>()
            .AsSingle()
            .NonLazy();

        private void BindGameStateMachine() => Container.Bind<GameStateMachine>()
            .FromInstance(_game.StateMachine)
            .AsSingle();

        private void BindCoroutineRunner() => Container.Bind<ICoroutineRunner>()
            .FromInstance(this)
            .AsSingle()
            .NonLazy();

        private void BindAssetProvider() => Container.Bind<IAssetProvider>()
            .To<AssetProvider>()
            .AsSingle()
            .NonLazy();

        private async Task BindStaticDataService()
        {
            Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle()
                .NonLazy();
            var service = Container.Resolve<IStaticDataService>();
            await service.Load();
        }

        private async Task BindSceneReferencesSO()
        {
            var sceneLoaderReferencesSO = await LoadSceneLoaderReferencesSO(Container);
            Container.Bind<SceneLoaderReferencesSO>()
                .FromInstance(sceneLoaderReferencesSO)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader() => Container.Bind<ISceneLoader>()
            .To<SceneLoader>()
            .AsSingle()
            .NonLazy();

        private void BindGame() => Container.Bind<Game>()
            .AsSingle()
            .NonLazy();

        private void BindGameFactory() =>
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle()
                .NonLazy();

        private void BindUIFactory() =>
            Container.Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle()
                .NonLazy();

        private async Task BindLoadingSceneCurtain()
        {
            var curtain = await CreateLoadingSceneCurtain(Container);
            Container.Bind<ISceneLoadingCurtain>()
                .FromInstance(curtain)
                .AsSingle()
                .NonLazy();
        }

        private async Task<LoadingSceneCurtain> CreateLoadingSceneCurtain(DiContainer container)
        {
            var assetProvider = container.Resolve<IAssetProvider>();
            var prefab = await assetProvider.LoadAs<LoadingSceneCurtain>(AssetsPaths.LoadingSceneCurtain);
            var loadingCurtain = Instantiate(prefab);
            loadingCurtain.HideForce();
            return loadingCurtain;
        }

        private async Task<SceneLoaderReferencesSO> LoadSceneLoaderReferencesSO(DiContainer container)
        {
            var so = await container.Resolve<IAssetProvider>()
                .LoadAs<SceneLoaderReferencesSO>(AssetsPaths.SceneReferencesSO);
            return so;
        }

        private void BindInputService()
        {
            Container.Bind<IInputService>()
                .FromMethod(RegisterInputService)
                .AsSingle()
                .NonLazy();
        }
        private IInputService RegisterInputService()
        {
            return Container.Instantiate<InputServiceBase>();
        }
    }
}