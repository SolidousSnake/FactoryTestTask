namespace _Project.Code.Runtime.Core.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}