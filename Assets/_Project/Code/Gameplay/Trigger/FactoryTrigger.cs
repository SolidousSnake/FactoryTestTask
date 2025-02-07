using _Project.Code.Core.Factory;
using _Project.Code.Gameplay.Worker;
using _Project.Code.Provider;
using UnityEngine;

namespace _Project.Code.Gameplay.Trigger
{
    public class FactoryTrigger : MonoBehaviour
    {
        [SerializeField] private Collider _trigger;
      
        private ResourceFactory _factory;
        private CollectedResourceProvider _provider;

        private void OnValidate()
        {
            _trigger ??= GetComponent<Collider>();
            _trigger.isTrigger = true;
        }

        public void Initialize(ResourceFactory factory, CollectedResourceProvider provider)
        {
            _factory = factory;
            _provider = provider;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WorkerFacade _)) 
                _provider.SetResource(_factory.GetResource());
        }
    }
}