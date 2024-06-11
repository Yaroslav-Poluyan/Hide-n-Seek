using System;
using _CodeBase.Services.LevelsData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.Infrastructure.GameplayLogic.MainMenu
{
    public class LevelBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Initialize(LevelData levelData)
        {
            _levelName.text = levelData.imageName;
        }

        private void OnClicked()
        {
           print("Clicked");
        }
    }
}