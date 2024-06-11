using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _CodeBase.Services.LevelsData
{
    public interface ILevelsDataService
    {
        public List<LevelData> GetLevelDatas();
        Task LoadLevelsDataAsync(CancellationToken cancellationToken);
    }
}