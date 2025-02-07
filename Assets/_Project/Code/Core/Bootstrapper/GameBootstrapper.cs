using _Project.Code.Core.Factory;
using _Project.Code.Data.Config;
using _Project.Code.Gameplay;
using _Project.Code.Gameplay.States;
using _Project.Code.Gameplay.Trigger;
using _Project.Code.Gameplay.Worker;
using _Project.Code.Provider;
using _Project.Code.Runtime.Core.StateMachine;
using _Project.Code.UI.UIButton;
using _Project.Code.UI.View;
using _Project.Code.UI.View.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Core.Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Player")] 
        [SerializeField] private WorkerConfig _workerConfig;
        [SerializeField] private WorkerFacade _workerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform[] _wayPoints;

        [Header("Map")] 
        [SerializeField] private Warehouse[] _warehouses;
        [SerializeField] private MiniGameTrigger _miniGameTrigger;
        [SerializeField] private FactoryTrigger _factoryTrigger;
        
        [Header("Common")] 
        [SerializeField] private float _factoryProducingDelay;
        [SerializeField] private ResourceConfig[] _resources;

        [Header("UI")] [SerializeField] private WorkerButton _workerButton;
        [SerializeField] private PlayingStateView _playingStateView;
        [SerializeField] private MiniGameStateView _miniGameStateView;
        [SerializeField] private StorageView _storageView;
        [SerializeField] private FactoryView _factoryView;

        private ResourceFactory _factory;
        private WorkerFacade _workerFacade;
        
        private void Awake()
        {
            var storage = new ResourcesStorage();
            var levelFsm = new LevelStateMachine();
            var provider = new CollectedResourceProvider();
            _factory = new ResourceFactory(_resources, _factoryProducingDelay);

            InitializePlayer(provider);
            InitializeWarehouses(storage);
            InitializeUI(storage);
            InitializeTriggers(levelFsm, provider);

            CreateStates(levelFsm, provider, storage);
            
            levelFsm.Enter<PlayingState>();
            _factory.StartCreating().Forget();
        }

        private void InitializePlayer(CollectedResourceProvider provider)
        {
            _workerFacade = Instantiate(_workerPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _workerFacade.Initialize(_workerConfig, _wayPoints, _spawnPoint, provider);
        }

        private void InitializeWarehouses(ResourcesStorage storage)
        {
            for (int i = 0; i < _warehouses.Length; i++) 
                _warehouses[i].Initialize(storage);
        }

        private void InitializeUI(ResourcesStorage storage)
        {
            _miniGameStateView.Initialize();
            _storageView.Initialize(storage);
            _factoryView.Initialize(_factory);
            _workerButton.Initialize(_workerFacade);
        }

        private void InitializeTriggers(LevelStateMachine levelFsm, CollectedResourceProvider provider)
        {
            _miniGameTrigger.Initialize(levelFsm);
            _factoryTrigger.Initialize(_factory, provider);
        }

        private void CreateStates(LevelStateMachine levelFsm, CollectedResourceProvider provider, ResourcesStorage storage)
        {
            levelFsm.RegisterState(new PlayingState(_playingStateView));
            levelFsm.RegisterState(new MiniGameState(levelFsm, _miniGameStateView, provider, storage, _workerFacade));
        }

        private void OnDestroy() => _factory.Stop();
    }
}