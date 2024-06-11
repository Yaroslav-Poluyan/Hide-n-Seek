using System.Collections.Generic;
using System.Threading.Tasks;

namespace _CodeBase.Services.LevelsData
{
    internal interface ILevelsDataService
    {
        public List<LevelData> GetLevelDatas();
        Task LoadLevelsDataAsync();
    }
}