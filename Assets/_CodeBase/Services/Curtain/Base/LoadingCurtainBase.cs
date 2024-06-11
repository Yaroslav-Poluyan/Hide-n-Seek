using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.Services.Curtain.Base
{
    public class LoadingCurtainBase : MonoBehaviour, IProgressCurtain
    {
        public Action AnimationsComplete { get; set; } = () => { };
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _progressSlider;
        private bool _isActive;
        private static readonly int ShowAnimation = Animator.StringToHash("Show");
        private static readonly int Hide1 = Animator.StringToHash("Hide");

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnStateChanged?.Invoke(_isActive);
            }
        }

        public Action<bool> OnStateChanged { get; set; }


        public void Show()
        {
            _progressSlider.value = 0f;
            gameObject.SetActive(true);
            PlayAnimation();
            SetProgress(0);
            IsActive = true;
        }

        public async Task Hide()
        {
            _animator.SetTrigger(Hide1);
            await Task.Delay((int)_animator.GetCurrentAnimatorStateInfo(0).length * 1000);
        }

        public void OnCloseAnimationComplete()
        {
            IsActive = false;
        }

        public void HideForce()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }

        private void PlayAnimation()
        {
            _animator.SetTrigger(ShowAnimation);
        }

        public void OnAnimationEnd()
        {
            print("OnAnimationEnd");
            AnimationsComplete?.Invoke();
            AnimationsComplete = () => { };
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SetProgress(0);
        }
    }
}