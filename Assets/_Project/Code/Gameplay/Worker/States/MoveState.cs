using _Project.Code.Gameplay.Worker.Movement;
using _Project.Code.Runtime.Core.StateMachine;

namespace _Project.Code.Gameplay.Worker.States
{
    public class MoveState : IUpdateableState
    {
        private readonly StateMachine _fsm;
        private readonly WaypointMovement _movement;

        public MoveState(StateMachine fsm, WaypointMovement movement)
        {
            _fsm = fsm;
            _movement = movement;
        }

        public void Enter()
        {
            _movement.ReachedFinalWaypoint += _fsm.Enter<IdleState>;
            _movement.Reset();
        }

        public void Update() => _movement.Move();

        public void Exit() => _movement.ReachedFinalWaypoint -= _fsm.Enter<IdleState>;
    }
}