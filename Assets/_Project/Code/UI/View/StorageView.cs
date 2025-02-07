using _Project.Code.Data.Enum;
using _Project.Code.Gameplay;
using UnityEngine;
using System;
using TMPro;

namespace _Project.Code.UI.View
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _woodLabel;
        [SerializeField] private TextMeshProUGUI _stoneLabel;
        [SerializeField] private TextMeshProUGUI _metalLabel;

        private ResourcesStorage _storage;

        public void Initialize(ResourcesStorage storage)
        {
            _storage = storage;
            _storage.BalanceChanged += SetAmount;
        }

        private void SetAmount(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Wood: _woodLabel.text = $"Wood: {amount}"; break;
                case ResourceType.Stone: _stoneLabel.text = $"Stone: {amount}"; break;
                case ResourceType.Metal: _metalLabel.text = $"Metal: {amount}"; break;
                default: throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }

        private void OnDestroy() => _storage.BalanceChanged -= SetAmount;
    }
}