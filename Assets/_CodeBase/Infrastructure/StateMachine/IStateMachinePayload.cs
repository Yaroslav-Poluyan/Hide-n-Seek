using _CodeBase.Infrastructure.StateMachine.States.Common;

namespace _CodeBase.Infrastructure.StateMachine
{
    /// <summary>
    /// Defines a method for <see cref="Enter{TState, TPayload}"/> the state with payload.
    /// </summary>
    public interface IStateMachinePayload
    {
        /// <summary>
        /// Switches to the specified state with payload.
        /// </summary>
        /// <param name="payload">Payload value.</param>
        /// <typeparam name="TState">State type. Class must be implement <see cref="IPayloadState{T}"/>.</typeparam>
        /// <typeparam name="TPayload">Payload type.</typeparam>
        /// <example><c>StateMachine.Enter&lt;MyState, string&gt;("payload");</c></example>
        /// <seealso cref="IPayloadState{T}"/>
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}