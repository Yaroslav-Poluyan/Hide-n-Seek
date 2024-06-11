using _CodeBase.Infrastructure.StateMachine.StateMediators;
using _CodeBase.Services.LevelsData;
using UnityEngine;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers.MainMenu
{
    public class MainMenuSceneController : SceneControllerBase
    {
        [SerializeField] private MainMenuStateMediator _mainMenuStateMediator;
        public void OnLevelBlockPressed(LevelData levelData)
        {
            _mainMenuStateMediator.OnLevelBlockPressed(levelData);
        }
    }
}