using System.Threading.Tasks;
using _CodeBase.Infrastructure.GameplayLogic.MainMenu;
using _CodeBase.Services;
using _CodeBase.Services.LevelsData;
using UnityEngine;

namespace _CodeBase.Infrastructure.Factory.UIFactory
{
    public interface IUIFactory : IService
    {
        Task<Transform> CreateLevelRow(Transform rowsParent);
        Task<LevelBlock> CreateLevelBlock(Transform rowTransform);
    }
}