using _Project.Code.Gameplay.Worker.Movement;
using _Project.Code.Runtime.Core.StateMachine;

namespace _Project.Code.Gameplay.Worker.States
{
    public class GoToSpawnPointState : IUpdateableState
    {
        private readonly StateMachine _fsm;
        private readonly VectorMovement _movement;

        public GoToSpawnPointState(StateMachine fsm, VectorMovement movement)
        {
            _fsm = fsm;
            _movement = movement;
        }

        public void Enter() => _movement.Reached += _fsm.Enter<IdleState>;

        public void Update() => _movement.Move();

        public void Exit() => _movement.Reached += _fsm.Enter<IdleState>;
    }
}