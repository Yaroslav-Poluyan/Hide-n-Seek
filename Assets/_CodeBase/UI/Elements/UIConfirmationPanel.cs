using System;
using _CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace _CodeBase.UI.Elements
{
    public abstract class UIConfirmationPanelBase : MonoBehaviour
    {
        [Inject] protected IInputService InputService;
        public Action OnClosed { get; set; }
        public Action OnOpened { get; set; }

        public void SetVisiblityState(bool b)
        {
            gameObject.SetActive(b);
            if (!b) OnClosed?.Invoke();
            else OnOpened?.Invoke();
        }

        protected abstract void Canceled();

        protected abstract void Confirmed();
    }
}