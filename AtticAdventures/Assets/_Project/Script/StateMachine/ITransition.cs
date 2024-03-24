namespace AtticAdventures.StateMachine
{
    public interface ITransition
    {
        public IState To { get; }
        public IPredicate Condition { get; }
    }
}
