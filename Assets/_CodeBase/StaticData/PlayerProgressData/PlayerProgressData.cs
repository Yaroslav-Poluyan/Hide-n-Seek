using _CodeBase.Services.SaveLoad;
using _CodeBase.StaticData.StaticData;

namespace _CodeBase.StaticData.PlayerProgressData
{
    public partial class PlayerProgressData : ISaveLoadOperator
    {
        public int Glory { get; set; }
        public bool HasChoosedGladiator { get; set; }
        public bool IsInitialTutorialPassed { get; set; }

        public PlayerProgressData(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
    }
}