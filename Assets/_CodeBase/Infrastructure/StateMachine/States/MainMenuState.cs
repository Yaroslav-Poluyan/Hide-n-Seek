using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.Services.Curtain;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class MainMenuState : ExitableStateBase, IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneLoadingCurtain _sceneLoadingCurtain;

        public MainMenuState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            ISceneLoadingCurtain sceneLoadingCurtain) : base(gameStateMachine)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneLoadingCurtain = sceneLoadingCurtain;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneType.MainMenu, OnLoaded);
        }

        private void OnLoaded()
        {
        }

        public void OnUIBlocksInitialized()
        {
            _sceneLoadingCurtain.Hide();
        }
    }
}