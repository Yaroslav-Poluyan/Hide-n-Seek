using _CodeBase.Infrastructure.StateMachine.States.Common;

namespace _CodeBase.Infrastructure.StateMachine
{
    /// <summary>
    /// Defines a method for <see cref="Enter{TState}"/> the state.
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// Switches to the specified state.
        /// </summary>
        /// <typeparam name="TState">State type. Class must be implement <see cref="IState"/>.</typeparam>
        /// <seealso cref="IState"/>
        void Enter<TState>() where TState : class, IState;
    }
}