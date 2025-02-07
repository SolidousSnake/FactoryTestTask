using _Project.Code.Data.Enum;
using _Project.Code.Provider;
using TMPro;
using UnityEngine;

namespace _Project.Code.UI.View.Label
{
    public class CollectedResourceLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _label;

        private CollectedResourceProvider _provider;

        public void Initialize(CollectedResourceProvider provider)
        {
            _provider = provider;
            _provider.Collected += SetAmount;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _label.text = $"";
        }
        
        private void SetAmount(ResourceType obj) => _label.text = $"Carrying {obj}";
        private void OnDestroy() => _provider.Collected -= SetAmount;
    }
}