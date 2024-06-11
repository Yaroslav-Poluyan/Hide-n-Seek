using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _CodeBase.Infrastructure.Factory.InfrastructureFactories;
using _CodeBase.Infrastructure.StateMachine.States;
using _CodeBase.Infrastructure.StateMachine.States.Common;

namespace _CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IStateMachine, IStateMachinePayload
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public TState GetState<TState>() where TState : class, IExitableState
        {
            if (!_states.ContainsKey(typeof(TState)))
            {
                throw new ArgumentException($"State of type {typeof(TState)} not found");
            }

            return _states[typeof(TState)] as TState;
        }

        public Type CurrentStateType => _currentState.GetType();

        public GameStateMachine(IInfrastructureFactory factory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = factory.CreateState<BootstrapState>(this),
                [typeof(MainMenuState)] = factory.CreateState<MainMenuState>(this),
                [typeof(LoadLevelState)] = factory.CreateState<LoadLevelState>(this),
                [typeof(GameLoopState)] = factory.CreateState<GameLoopState>(this),
            };
        }

        public async void Enter<TState>() where TState : class, IState
        {
            if (_currentState != null && typeof(TState) == CurrentStateType) return;
            IState state = await ChangeState<TState>();
            state.Enter();
        }
        

        public async void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            if (_currentState != null && typeof(TState) == CurrentStateType) return;
            TState state = await ChangeState<TState>();
            state.Enter(payload);
        }
        

        public void RegisterOnExitTask<TState>(Func<Task> task) where TState : class, IExitableState
        {
            var state = GetState<TState>();
            state.RegisterOnExitTask(task);
        }

        private async Task<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (_currentState != null) await _currentState.Exit();
            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }
    }
}