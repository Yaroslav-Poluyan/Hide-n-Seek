using _CodeBase.Infrastructure.StateMachine.StateMediators;
using _CodeBase.Infrastructure.StateMachine.States;
using _CodeBase.Services.LevelsData;
using Zenject;
using PlayerProgressData = _CodeBase.StaticData.PlayerProgressData.PlayerProgressData;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers.MainMenu
{
    public class MainMenuStateMediator : StateMediatorBase<MainMenuState>
    {
        [Inject] private PlayerProgressData _playerProgressData;

        protected override void Initialization()
        {
        }

        public void OnUIBlocksInitialized()
        {
            state.OnUIBlocksInitialized();
        }

        public void OnLevelBlockPressed(LevelData levelData)
        {
            StateMachine.Enter<LoadLevelState, LevelData>(levelData);
        }
    }
}