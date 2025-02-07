using _Project.Code.Gameplay.Worker;
using _Project.Code.Provider;
using _Project.Code.Runtime.Core.StateMachine;
using _Project.Code.UI.View.States;

namespace _Project.Code.Gameplay.States
{
    public class MiniGameState : IState
    {
        private readonly LevelStateMachine _fsm;
        private readonly MiniGameStateView _stateView;
        private readonly CollectedResourceProvider _provider;
        private readonly ResourcesStorage _storage;
        private readonly WorkerFacade _workerFacade;
        
        public MiniGameState(LevelStateMachine fsm
            , MiniGameStateView stateView
            , CollectedResourceProvider provider
            , ResourcesStorage storage
            , WorkerFacade workerFacade)
        {
            _fsm = fsm;
            _stateView = stateView;
            _provider = provider;
            _storage = storage;
            _workerFacade = workerFacade;
        }

        public async void Enter()
        {
            _workerFacade.Chill();

            var result = await _stateView.Open();
            
            if (result == _provider.CollectedResource)
                _storage.Add(result, 1);
            
            _fsm.Enter<PlayingState>();
            _workerFacade.ReturnToSpawnPoint();
        }

        public void Exit() => _stateView.Close();
    }
}