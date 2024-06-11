using _CodeBase.Infrastructure.Factory.Game;
using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.Services.Curtain;
using _CodeBase.StaticData.StaticData;
#if UNITY_EDITOR
#endif

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : ExitableStateBase
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoaderReferencesSO _sceneLoaderReferencesSO;
        private readonly ISceneLoadingCurtain _sceneLoadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IStaticDataService staticDataService, IGameFactory gameFactory,
            SceneLoaderReferencesSO sceneLoaderReferencesSO,
            ISceneLoadingCurtain sceneLoadingCurtain) : base(gameStateMachine)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _sceneLoaderReferencesSO = sceneLoaderReferencesSO;
            _sceneLoadingCurtain = sceneLoadingCurtain;
        }
    }
}