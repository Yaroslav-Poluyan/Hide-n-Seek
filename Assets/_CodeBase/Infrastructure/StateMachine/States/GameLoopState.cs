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
        private readonly PlayerProgressData _playerProgressData;


        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory,
            ISceneLoadingCurtain sceneLoadingCurtain,
            PlayerProgressData playerProgressData) : base(stateMachine)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _sceneLoadingCurtain = sceneLoadingCurtain;
            _playerProgressData = playerProgressData;
        }

        public override async Task Exit()
        {
            await base.Exit();
            Debug.Log("Exit GameLoopState");
        }

        public void Enter()
        {
            _sceneLoadingCurtain.Hide();
            Debug.Log("Enter GameLoopState");
        }

        public void OnImageLoaded()
        {
            _sceneLoadingCurtain.Hide();
        }
    }
}