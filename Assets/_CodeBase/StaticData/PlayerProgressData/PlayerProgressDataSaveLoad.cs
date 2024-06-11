using System;
using System.Collections.Generic;
using _CodeBase.Services.SaveLoad;
using _CodeBase.StaticData.StaticData;

namespace _CodeBase.StaticData.PlayerProgressData
{
    public partial class PlayerProgressData
    {
        private readonly IStaticDataService _staticDataService;
        public string MainDataKey => "LevelProgresses";

        public void LoadAll()
        {
            _levelProgresses = SaveLoadService.Load<List<LevelProgress>>(MainDataKey);
            if (_levelProgresses == null)
            {
                _levelProgresses = new List<LevelProgress>();

                foreach (var templateLevelData in _levelsDataService.GetLevelDatas())
                {
                    _levelProgresses.Add(new LevelProgress
                    {
                        _levelId = templateLevelData.Id,
                        _currentCounter = templateLevelData.counter,
                        _isCompleted = false
                    });
                }
            }
        }

        public void SaveAll()
        {
            SaveLoadService.Save(MainDataKey, _levelProgresses);
        }
    }
}