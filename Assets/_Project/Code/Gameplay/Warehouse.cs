using System;
using _Project.Code.Data.Enum;
using UnityEngine;

namespace _Project.Code.Gameplay
{
    public class Warehouse : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnStep;
        [SerializeField] private ResourceType _resourceType;

        private ResourcesStorage _storage;
        private float _yOffset;

        public void Initialize(ResourcesStorage storage)
        {
            _storage = storage;
            _storage.BalanceChanged += Put;
        }

        private void Put(ResourceType resourceType, int amount)
        {
            if(resourceType != _resourceType)
                return;

            for (int i = 0; i < amount; i++)
            {
                var spawnPosition = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + _yOffset, _spawnPoint.position.z);
                _yOffset += _spawnStep;
                Instantiate(_prefab, spawnPosition, Quaternion.identity, _spawnPoint);
            }
        }

        private void OnDestroy() => _storage.BalanceChanged -= Put;
    }
}