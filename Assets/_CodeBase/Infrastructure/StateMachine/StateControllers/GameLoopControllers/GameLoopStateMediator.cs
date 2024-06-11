using _CodeBase.Infrastructure.StateMachine.States;
using _CodeBase.Services.Curtain;
using _CodeBase.StaticData.PlayerProgressData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers.GameLoopControllers
{
    public class GameLoopStateMediator : StateMediatorBase<GameLoopState>
    {
        [Inject] private PlayerProgressData _playerProgressData;
        [Inject] private ISceneLoadingCurtain _sceneLoadingCurtain;

        protected override void Initialization()
        {
        }

        public void OnImageReady()
        {
            state.OnImageLoaded();
        }

        public async void OnLevelCompleted()
        {
            await _sceneLoadingCurtain.Show();
            StateMachine.Enter<MainMenuState>();
        }
    }
}