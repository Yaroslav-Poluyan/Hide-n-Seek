using _CodeBase.Infrastructure.Factory.Game;
using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.Services.Curtain;
using _CodeBase.Services.LevelController;
using _CodeBase.Services.LevelsData;
using _CodeBase.StaticData.StaticData;
#if UNITY_EDITOR
#endif

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : ExitableStateBase, IPayloadState<LevelData>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoaderReferencesSO _sceneLoaderReferencesSO;
        private readonly LevelController _levelController;
        private readonly ISceneLoadingCurtain _sceneLoadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, IGameFactory gameFactory,
            SceneLoaderReferencesSO sceneLoaderReferencesSO, LevelController levelController,
            ISceneLoadingCurtain sceneLoadingCurtain) : base(gameStateMachine)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _gameFactory = gameFactory;
            _sceneLoaderReferencesSO = sceneLoaderReferencesSO;
            _levelController = levelController;
            _sceneLoadingCurtain = sceneLoadingCurtain;
        }

        public async void Enter(LevelData payload)
        {
            _levelController.SetCurrentLevel(payload);
            await _sceneLoadingCurtain.Show();
            _sceneLoader.Load(SceneType.Game, OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}