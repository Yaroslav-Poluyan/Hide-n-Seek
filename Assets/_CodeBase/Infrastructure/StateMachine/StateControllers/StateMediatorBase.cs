using _CodeBase.Infrastructure.StateMachine.States.Common;
using _CodeBase.Services.Curtain;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.StateMachine.StateControllers
{
    public abstract class StateMediatorBase<TState> : MonoBehaviour where TState : class, IExitableState
    {
        [Inject] protected GameStateMachine StateMachine;
        [Inject] protected ISceneLoadingCurtain SceneLoadingCurtain;
        protected TState state;

        protected virtual void Awake()
        {
            state = StateMachine.GetState<TState>();
            Initialization();
        }
        protected bool IsAnyCurtainActive => SceneLoadingCurtain.IsActive;
        protected abstract void Initialization();
    }
}