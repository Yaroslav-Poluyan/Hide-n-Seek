using System;
using System.Collections.Generic;
using _CodeBase.Services.LevelsData;
using _CodeBase.Services.SaveLoad;
using _CodeBase.StaticData.StaticData;

namespace _CodeBase.StaticData.PlayerProgressData
{
    public partial class PlayerProgressData : ISaveLoadOperator
    {
        private readonly ILevelsDataService _levelsDataService;
        private List<LevelProgress> _levelProgresses = new();

        [ES3Serializable]
        public class LevelProgress
        {
            public int _levelId;
            public int _currentCounter;
            public bool _isCompleted;
        }

        public PlayerProgressData(IStaticDataService staticDataService, ILevelsDataService levelsDataService)
        {
            _levelsDataService = levelsDataService;
            _staticDataService = staticDataService;
        }

        public void SetLevelData(LevelData levelData, int currentCounter, bool isCompleted)
        {
            var levelProgress = new LevelProgress
            {
                _levelId = levelData.Id,
                _currentCounter = currentCounter,
                _isCompleted = isCompleted
            };
            var idxInArray = _levelProgresses.FindIndex(x => x._levelId == levelData.Id);
            if (idxInArray == -1)
            {
                _levelProgresses.Add(levelProgress);
            }
            else
            {
                _levelProgresses[idxInArray] = levelProgress;
            }
        }

        public List<LevelProgress> GetLevelProgresses() => _levelProgresses;

        public int IncrementCounter(int id)
        {
            var idxInArray = _levelProgresses.FindIndex(x => x._levelId == id);
            if (idxInArray == -1)
            {
                throw new Exception("Level progress not found");
            }

            _levelProgresses[idxInArray]._currentCounter++;
            return _levelProgresses[idxInArray]._currentCounter;
        }

        public int DecrementCounter(int id)
        {
            var idxInArray = _levelProgresses.FindIndex(x => x._levelId == id);
            if (idxInArray == -1)
            {
                throw new Exception("Level progress not found");
            }

            _levelProgresses[idxInArray]._currentCounter--;
            if (_levelProgresses[idxInArray]._currentCounter < 0)
            {
                _levelProgresses[idxInArray]._currentCounter = 0;
            }

            return _levelProgresses[idxInArray]._currentCounter;
        }

        public void SetPassedLevel(int id)
        {
            var idxInArray = _levelProgresses.FindIndex(x => x._levelId == id);
            if (idxInArray == -1)
            {
                throw new Exception("Level progress not found");
            }

            _levelProgresses[idxInArray]._isCompleted = true;
        }
    }
}