using System.Collections.Generic;
using System.Threading.Tasks;
using _CodeBase.Infrastructure.Factory.UIFactory;
using _CodeBase.Services.LevelsData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.GameplayLogic.MainMenu
{
    public class UILevelsPanelController : MonoBehaviour
    {
        [SerializeField] private float _colsCount = 3;
        [SerializeField] private Transform _rowsParent;
        [SerializeField] private List<LevelBlock> _allCreatedBlocks;
        [Inject] private IUIFactory _uiFactory;
        [Inject] private ILevelsDataService _levelsDataService;

        private void Awake()
        {
            InitializeBlocks();
        }

        private async Task InitializeBlocks()
        {
            var levelDatas = _levelsDataService.GetLevelDatas();
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
                        var createdBlock = await _uiFactory.CreateLevelBlock(row);
                        createdBlock.Initialize(levelData);
                        _allCreatedBlocks.Add(createdBlock);
                    }
                }
            }
        }
    }
}