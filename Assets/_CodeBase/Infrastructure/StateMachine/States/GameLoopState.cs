using System.Threading.Tasks;
using _CodeBase.Infrastructure.Factory.Game;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.Services;
using _CodeBase.Services.Curtain;
using UnityEngine;
using PlayerProgressData = _CodeBase.StaticData.PlayerProgressData.PlayerProgressData;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class GameLoopState : ExitableStateBase, IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoadingCurtain _sceneLoadingCurtain;
        private readonly TimeManagerService _timeManagerService;
        private readonly PlayerProgressData _playerProgressData;


        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory,
            ISceneLoadingCurtain sceneLoadingCurtain, TimeManagerService timeManagerService,
            PlayerProgressData playerProgressData) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _sceneLoadingCurtain = sceneLoadingCurtain;
            _timeManagerService = timeManagerService;
            _playerProgressData = playerProgressData;
        }

        public override async Task Exit()
        {
            await base.Exit();
            Debug.Log("Exit GameLoopState");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Enter()
        {
            Debug.Log("Enter GameLoopState");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _sceneLoadingCurtain.Hide();
        }
    }
}