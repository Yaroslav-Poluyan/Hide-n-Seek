using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers.GameLoopControllers
{
    public class MainGameImage : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameLoopSceneController _gameLoopSceneController;
        [SerializeField] private Image _image;

        public void OnPointerClick(PointerEventData eventData)
        {
            _gameLoopSceneController.OnImageClick();
        }
    }
}