using _Project.Code.Core.Factory;
using _Project.Code.Data.Enum;
using TMPro;
using UnityEngine;

namespace _Project.Code.UI.View
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _resourceLabel;
        
        private ResourceFactory _factory;

        public void Initialize(ResourceFactory factory)
        {
            _factory = factory;
            _factory.Created += UpdateLabel;
        }

        private void UpdateLabel(ResourceType obj) => _resourceLabel.text = $"Created: {obj}";

        private void OnDestroy() => _factory.Created -= UpdateLabel;
    }
}