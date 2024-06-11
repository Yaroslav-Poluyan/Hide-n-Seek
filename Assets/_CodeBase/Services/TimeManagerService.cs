using _CodeBase.Infrastructure;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace _CodeBase.Services
{
    public class TimeManagerService : MonoBehaviour
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _smoothTimeScaleCoroutine;
        private MMTimeManager _timeManager;

        private void Awake()
        {
            _timeManager = gameObject.AddComponent<MMTimeManager>();
        }

        public TimeManagerService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void SetTimeScale(float timeScale)
        {
            _timeManager.SetTimescaleTo(timeScale);
        }
    }
}