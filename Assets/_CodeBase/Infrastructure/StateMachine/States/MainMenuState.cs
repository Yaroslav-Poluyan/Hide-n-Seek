using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine.States.Common;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class MainMenuState : ExitableStateBase, IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public MainMenuState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader) : base(gameStateMachine)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneType.MainMenu, OnLoaded);
        }

        private void OnLoaded()
        {
        }
    }
}