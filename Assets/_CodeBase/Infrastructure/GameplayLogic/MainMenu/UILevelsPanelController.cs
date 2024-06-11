using _CodeBase.Infrastructure.Factory.UIFactory;
using _CodeBase.Services.LevelsData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.GameplayLogic.MainMenu
{
    public class UILevelsPanelController : MonoBehaviour
    {
        [SerializeField] private Transform _parentOfBlocks;
        [Inject] private IUIFactory _uiFactory;
        [Inject] private ILevelsDataService _levelsDataService;
    }
}