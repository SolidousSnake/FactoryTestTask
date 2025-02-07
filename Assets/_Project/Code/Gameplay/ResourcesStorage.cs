using _Project.Code.Data.Enum;
using System.Collections.Generic;
using System;

namespace _Project.Code.Gameplay
{
    public class ResourcesStorage
    {
        private readonly Dictionary<ResourceType, int> _storage = new();

        public event Action<ResourceType, int> BalanceChanged;

        public void Add(ResourceType resource, int amount)
        {
            _storage.TryAdd(resource, 0);
            _storage[resource] += amount;
            BalanceChanged?.Invoke(resource, _storage[resource]);
        }

        public bool TryGet(ResourceType type, int amount)
        {
            if (type == ResourceType.None)
                throw new ArgumentException("Currency type cannot be 'None'.", nameof(type));

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount to spend cannot be negative.");


            if (_storage[type] < amount)
                return false;

            _storage[type] -= amount;
            BalanceChanged?.Invoke(type, _storage[type]);
            return true;
        }
    }
}