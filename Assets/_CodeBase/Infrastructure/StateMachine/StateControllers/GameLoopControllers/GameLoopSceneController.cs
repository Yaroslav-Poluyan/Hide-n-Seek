using System;
using System.Linq;
using System.Threading.Tasks;
using _CodeBase.Services.LevelController;
using _CodeBase.Services.WebRequests;
using _CodeBase.StaticData.PlayerProgressData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers.GameLoopControllers
{
    public class GameLoopSceneController : MonoBehaviour
    {
        [SerializeField] private GameLoopStateMediator _gameLoopStateMediator;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _clickCounterText;
        [SerializeField] private Button _closeButton;
        [Inject] private LevelController _levelController;
        [Inject] private IImageDownloaderService _imageDownloaderService;
        [Inject] private PlayerProgressData _playerProgressData;

        private void Awake()
        {
            Initialize();
            _closeButton.onClick.AddListener(OnLevelCompleted);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnLevelCompleted);
        }

        private void OnLevelCompleted()
        {
            _playerProgressData.SaveAll();
            _gameLoopStateMediator.OnLevelCompleted();
        }

        private async Task Initialize()
        {
            var currentLevelData = _levelController.CurrentLevelData;
            var saveData = _playerProgressData.GetLevelProgresses()
                .FirstOrDefault(x => x._levelId == currentLevelData.Id);
            var image = await _imageDownloaderService.GetSprite(currentLevelData.imageUrl);
            _image.sprite = image;
            RefreshCounter(saveData._currentCounter);
            _gameLoopStateMediator.OnImageReady();
        }

        public void OnImageClick()
        {
            var estimated = _playerProgressData.DecrementCounter(_levelController.CurrentLevelData.Id);
            RefreshCounter(_playerProgressData.GetLevelProgresses()
                .FirstOrDefault(x => x._levelId == _levelController.CurrentLevelData.Id)!._currentCounter);
            if (estimated == 0)
            {
                _playerProgressData.SetPassedLevel(_levelController.CurrentLevelData.Id);
                OnLevelCompleted();
            }
        }

        private void RefreshCounter(int counter)
        {
            _clickCounterText.text = counter.ToString();
        }
    }
}