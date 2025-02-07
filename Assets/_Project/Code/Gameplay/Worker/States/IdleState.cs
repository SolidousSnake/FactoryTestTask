using _Project.Code.Runtime.Core.StateMachine;
using _Project.Code.UI.View.Label;

namespace _Project.Code.Gameplay.Worker.States
{
    public class IdleState : IState
    {
        private readonly CollectedResourceLabel _label;

        public IdleState(CollectedResourceLabel label)
        {
            _label = label;
        }

        public void Enter() => _label.Close();

        public void Exit() => _label.Open();
    }
}