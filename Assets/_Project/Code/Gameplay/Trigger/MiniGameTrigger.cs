using _Project.Code.Gameplay.States;
using _Project.Code.Gameplay.Worker;
using _Project.Code.Runtime.Core.StateMachine;
using UnityEngine;

namespace _Project.Code.Gameplay.Trigger
{
    public class MiniGameTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _trigger;

        private LevelStateMachine _levelFsm;

        private void OnValidate()
        {
            _trigger ??= GetComponent<Collider>();
            _trigger.isTrigger = true;
        }

        public void Initialize(LevelStateMachine fsm)
        {
            _levelFsm = fsm;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_levelFsm is null)
                return;
            
            if (other.TryGetComponent(out WorkerFacade _))
                _levelFsm.Enter<MiniGameState>();
        }
    }
}