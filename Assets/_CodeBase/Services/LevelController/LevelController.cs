using _CodeBase.Services.LevelsData;

namespace _CodeBase.Services.LevelController
{
    public class LevelController
    {
        public LevelData CurrentLevelData { get; private set; }

        public void SetCurrentLevel(LevelData payload)
        {
            CurrentLevelData = payload;
        }
    }
}