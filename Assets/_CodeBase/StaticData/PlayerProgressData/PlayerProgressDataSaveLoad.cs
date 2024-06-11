using System;
using _CodeBase.StaticData.StaticData;

namespace _CodeBase.StaticData.PlayerProgressData
{
    public partial class PlayerProgressData
    {
        private readonly IStaticDataService _staticDataService;
        public string MainDataKey => "player_progress";

        public void SaveAll()
        {
        }

        public void LoadAll()
        {
        }
    }
}