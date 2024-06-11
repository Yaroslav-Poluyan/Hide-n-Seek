using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.UI.Elements
{
    public class UIFillSlider : MonoBehaviour
    {
        [SerializeField] private Image _frontImage;
        [SerializeField] private Image _backImage;
        public float CurrentValue => _frontImage.fillAmount;

        public void SetVaue(float value)
        {
            _frontImage.fillAmount = value;
        }

        public void AddValueInPercents(float stepValueInPercent)
        {
            _frontImage.fillAmount += stepValueInPercent;
        }
    }
}