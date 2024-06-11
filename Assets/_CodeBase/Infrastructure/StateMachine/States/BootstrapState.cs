using _CodeBase.Infrastructure.SceneLoading;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.StaticData.StaticData;
using PlayerProgressData = _CodeBase.StaticData.PlayerProgressData.PlayerProgressData;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : ExitableStateBase, IState
    {
        private const SceneType Bootstrapper = SceneType.Bootstrapper;
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly PlayerProgressData _playerProgressData;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData,
            PlayerProgressData playerProgressData) :
            base(stateMachine)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _playerProgressData = playerProgressData;
        }

        public void Enter()
        {
            LoadData();
            _sceneLoader.Load(Bootstrapper, onLoaded: EnterMainMenu);
        }

        private void EnterMainMenu()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        private void LoadData()
        {
            _playerProgressData.LoadAll();
        }
    }
}