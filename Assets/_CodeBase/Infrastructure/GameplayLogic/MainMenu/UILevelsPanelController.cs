using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _CodeBase.Infrastructure.Factory.UIFactory;
using _CodeBase.Infrastructure.StateMachine.StateControllers.MainMenu;
using _CodeBase.Services.LevelsData;
using _CodeBase.Services.WebRequests;
using _CodeBase.StaticData.PlayerProgressData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.GameplayLogic.MainMenu
{
    public class UILevelsPanelController : MonoBehaviour
    {
        [SerializeField] private MainMenuSceneController _mainMenuSceneController;
        [SerializeField] private MainMenuStateMediator _mainMenuStateMediator;
        [SerializeField] private float _colsCount = 3;
        [SerializeField] private Transform _rowsParent;
        [SerializeField] private List<LevelBlock> _allCreatedBlocks;
        [Inject] private IUIFactory _uiFactory;
        [Inject] private ILevelsDataService _levelsDataService;
        [Inject] private PlayerProgressData _playerProgressData;
        [Inject] private IImageDownloaderService _imageDownloaderService;

        private void Start()
        {
            InitializeBlocks();
        }

        private void OnDestroy()
        {
            foreach (var block in _allCreatedBlocks)
            {
                block.OnPressed -= _mainMenuSceneController.OnLevelBlockPressed;
            }
        }

        private async Task InitializeBlocks()
        {
            await SpawnBlocks();
            await CheckForAccessibility();
            print("Blocks initialized");
            _mainMenuStateMediator.OnUIBlocksInitialized();
        }

        private async Task CheckForAccessibility()
        {
            var tasks = new List<Task<bool>>();
            foreach (var block in _allCreatedBlocks)
            {
                var task = _imageDownloaderService.CheckAccessToUrl(block.LinkedLevelData.imageUrl);
                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            for (int i = 0; i < _allCreatedBlocks.Count; i++)
            {
                var block = _allCreatedBlocks[i];
                var isAvailable = results[i];
                block.SetAvailable(isAvailable);
            }
        }

        private async Task SpawnBlocks()
        {
            var levelDatas = _levelsDataService.GetLevelDatas();
            var saveDatas = _playerProgressData.GetLevelProgresses();
            var rowsCount = Mathf.CeilToInt(levelDatas.Count / _colsCount);
            for (int i = 0; i < rowsCount; i++)
            {
                var row = await _uiFactory.CreateLevelRow(_rowsParent);
                for (int j = 0; j < _colsCount; j++)
                {
                    var index = i * (int)_colsCount + j;
                    if (index < levelDatas.Count)
                    {
                        var levelData = levelDatas[index];
                        var playerProgress = saveDatas.Find(x => x._levelId == levelData.Id);
                        var createdBlock = await _uiFactory.CreateLevelBlock(row);
                        createdBlock.Initialize(levelData, playerProgress);
                        createdBlock.OnPressed += _mainMenuSceneController.OnLevelBlockPressed;
                        createdBlock.SetInterractable(playerProgress._isCompleted);
                        _allCreatedBlocks.Add(createdBlock);
                    }
                }
            }
        }
    }
}