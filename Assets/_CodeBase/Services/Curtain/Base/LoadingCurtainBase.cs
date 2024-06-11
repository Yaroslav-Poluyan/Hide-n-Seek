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
        private static readonly int Showhash = Animator.StringToHash("Show");
        private static readonly int ShowForceHash = Animator.StringToHash("ShowForce");
        private static readonly int HideHash = Animator.StringToHash("Hide");
        private static readonly int HideForceHash = Animator.StringToHash("HideForce");

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

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SetProgress(0);
        }


        public void Show()
        {
            _progressSlider.value = 0f;
            gameObject.SetActive(true);
            _animator.SetTrigger(Showhash);
            SetProgress(0);
            IsActive = true;
        }

        public void ShowForce()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(ShowForceHash);
            SetProgress(0);
            IsActive = true;
        }

        public async Task Hide()
        {
            _animator.SetTrigger(HideHash);
            await Task.Delay((int)_animator.GetCurrentAnimatorStateInfo(0).length * 1000);
            gameObject.SetActive(false);
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
    }
}