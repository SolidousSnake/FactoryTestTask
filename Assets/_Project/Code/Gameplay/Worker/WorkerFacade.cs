using System.Collections.Generic;
using _Project.Code.Data.Config;
using _Project.Code.Gameplay.Worker.Movement;
using _Project.Code.Gameplay.Worker.States;
using _Project.Code.Provider;
using _Project.Code.Runtime.Core.StateMachine;
using _Project.Code.UI.View.Label;
using UnityEngine;

namespace _Project.Code.Gameplay.Worker
{
    public class WorkerFacade : MonoBehaviour
    {
        [SerializeField] private CollectedResourceLabel _label;
        
        private StateMachine _fsm;

        public void Initialize(WorkerConfig workerConfig
            , IReadOnlyList<Transform> waypoints
            , Transform spawnPoint
            , CollectedResourceProvider provider)
        {
            _fsm = new StateMachine();

            var waypointMovement = new WaypointMovement(transform, waypoints, workerConfig.MovementSpeed);
            var vectorMovement = new VectorMovement(transform, spawnPoint, workerConfig.MovementSpeed);
            
            _label.Initialize(provider);
            
            _fsm.RegisterState(new IdleState(_label));
            _fsm.RegisterState(new MoveState(_fsm, waypointMovement));
            _fsm.RegisterState(new GoToSpawnPointState(_fsm, vectorMovement));
            _fsm.Enter<IdleState>();
        }

        private void Update() => _fsm.Update();

        public void Chill() => _fsm.Enter<IdleState>();
        public void GoToFactory() => _fsm.Enter<MoveState>();
        public void ReturnToSpawnPoint() => _fsm.Enter<GoToSpawnPointState>();
    }
}