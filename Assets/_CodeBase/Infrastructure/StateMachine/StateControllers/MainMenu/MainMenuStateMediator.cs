using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;
using PlayerProgressData = _CodeBase.StaticData.PlayerProgressData.PlayerProgressData;

namespace _CodeBase.Infrastructure.StateMachine.StateMediators.MainMenu
{
    public class MainMenuStateMediator : StateMediatorBase<MainMenuState>
    {
        [Inject] private PlayerProgressData _playerProgressData;

        protected override void Initialization()
        {
        }
    }
}