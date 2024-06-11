using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _CodeBase.UI.Elements
{
    public class UIButtonBase : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private MMFeedbacks _onChosenFeedbacks;
        [SerializeField] private MMFeedbacks _onUnchosenFeedbacks;
        [SerializeField] private MMFeedbacks _onPressedFeedbacks;
        public UnityAction<UIButtonBase> OnSelected;
        public UnityAction<UIButtonBase> OnDeselected;
        public UnityAction<UIButtonBase> onButtonPressed;

        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        public bool IsSelected { get; set; }


        private void Awake()
        {
            //bind this event to the button
            _button.onClick.AddListener(() => onButtonPressed?.Invoke(this));
        }

        public void OnSelect(BaseEventData eventData)
        {
            print(gameObject.name + " was selected");
            SetSelectedManually();
            IsSelected = true;
            OnSelected?.Invoke(this);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            SetDeselectedManually();
            IsSelected = false;
            OnDeselected?.Invoke(this);
        }

        public void SetSelectedManually()
        {
            _onChosenFeedbacks.PlayFeedbacks();
        }

        public void SetDeselectedManually()
        {
            _onUnchosenFeedbacks.PlayFeedbacks();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            _onPressedFeedbacks.PlayFeedbacks();
        }

        public void InitializeFeedbacks()
        {
            _onChosenFeedbacks?.Initialization(gameObject);
            _onUnchosenFeedbacks?.Initialization(gameObject);
            _onPressedFeedbacks?.Initialization(gameObject);
        }
    }
}