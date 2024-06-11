using _CodeBase.Infrastructure.Factory.InfrastructureFactories;
using _CodeBase.Infrastructure.StateMachine;

namespace _CodeBase.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(IInfrastructureFactory factory)
        {
            StateMachine = new GameStateMachine(factory);
        }
    }
}