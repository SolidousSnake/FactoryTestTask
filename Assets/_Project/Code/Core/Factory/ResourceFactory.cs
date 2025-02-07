using System;
using _Project.Code.Data.Enum;
using _Project.Code.Data.Config;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Core.Factory
{
    public class ResourceFactory
    {
        private readonly ResourceConfig[] _resourceConfigs;
        private readonly Queue<ResourceType> _producedResources;
        private readonly float _creationDelay;

        private readonly CancellationTokenSource _cts;

        public ResourceFactory(ResourceConfig[] resourceConfigs, float creationDelay)
        {
            _resourceConfigs = resourceConfigs;
            _creationDelay = creationDelay;

            _producedResources = new Queue<ResourceType>();
            _cts = new CancellationTokenSource();
        }

        public event Action<ResourceType> Created;

        public async UniTask StartCreating()
        {
            while (!_cts.IsCancellationRequested)
            {
                var newResource = GetRandomResourceType();
                _producedResources.Enqueue(newResource);
                Created?.Invoke(newResource);
                await UniTask.WaitForSeconds(_creationDelay, ignoreTimeScale: false);
            }
        }

        public void Stop() => _cts.Dispose();

        public ResourceType GetResource() => _producedResources.Dequeue();
        public ResourceType GetLastResource() => _producedResources.Peek();
        private ResourceType GetRandomResourceType() => _resourceConfigs[Random.Range(0, _resourceConfigs.Length)].Type;
    }
}