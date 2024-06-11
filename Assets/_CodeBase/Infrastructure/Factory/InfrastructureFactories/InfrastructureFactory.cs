using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States.Common;
using Zenject;

namespace _CodeBase.Infrastructure.Factory.InfrastructureFactories
{
    /// <summary>
    /// Factory for infrastructure objects. Implemented dependency with Zenject.
    /// </summary>
    public class InfrastructureFactory : IInfrastructureFactory
    {
        private readonly DiContainer _container;

        public InfrastructureFactory(DiContainer container)
        {
            _container = container;
        }

        public T CreateState<T>(IStateMachine stateMachine) where T : IExitableState =>
            _container.Instantiate<T>(new[] {stateMachine});
    }
}