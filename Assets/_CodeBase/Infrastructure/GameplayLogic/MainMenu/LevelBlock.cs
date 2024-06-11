using System;
using _CodeBase.Services.LevelsData;
using _CodeBase.StaticData.PlayerProgressData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.Infrastructure.GameplayLogic.MainMenu
{
    public class LevelBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private TextMeshProUGUI _counter;
        [SerializeField] private TextMeshProUGUI _completed;
        [SerializeField] private Button _button;
        [SerializeField] private Image _unavailableImageAlarm;
        public LevelData LinkedLevelData { get; private set; }
        public Action<LevelData> OnPressed { get; set; }

        private void Start()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Initialize(LevelData levelData, PlayerProgressData.LevelProgress playerProgress)
        {
            LinkedLevelData = levelData;
            _levelName.text = levelData.imageName;
            _counter.text = playerProgress._currentCounter.ToString();
            _completed.text = playerProgress._isCompleted ? "Completed" : "Not completed";
        }

        private void OnClicked()
        {
            print("Clicked");
            OnPressed?.Invoke(LinkedLevelData);
        }

        public void SetInterractable(bool playerProgressIsCompleted)
        {
            _button.interactable = !playerProgressIsCompleted;
        }

        public void SetAvailable(bool isAvailable)
        {
            _unavailableImageAlarm.gameObject.SetActive(!isAvailable);
            _button.interactable = isAvailable;
        }
    }
}