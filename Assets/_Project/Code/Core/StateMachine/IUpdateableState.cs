namespace _Project.Code.Runtime.Core.StateMachine
{
    public interface IUpdateableState : IState
    {
        public void Update();
    }
}