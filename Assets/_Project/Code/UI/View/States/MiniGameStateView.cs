using System;
using System.Collections.Generic;
using _Project.Code.Data.Enum;
using _Project.Code.UI.UIButton;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.UI.View.States
{
    public class MiniGameStateView : MonoBehaviour
    {
        [SerializeField] private MiniGameButton _buttonPrefab;
        [SerializeField] private RectTransform _buttonParent;
        
        private UniTaskCompletionSource<ResourceType> _result;
        private readonly List<MiniGameButton> _buttons = new(); 
        
        public void Initialize()
        {
            var resources = Enum.GetValues(typeof(ResourceType));
            
            for (int i = 1; i < resources.Length; i++)
            {
                var button = Instantiate(_buttonPrefab, _buttonParent);
                var resource = (ResourceType)resources.GetValue(i);
                button.Initialize(resource, resource.ToString());
                
                button.onClick += HandleClick;
                _buttons.Add(button);
            }
        }
        
        public UniTask<ResourceType> Open()
        {
            gameObject.SetActive(true);
            _result = new UniTaskCompletionSource<ResourceType>();
            return _result.Task;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        private void HandleClick(ResourceType resource)
        {
            _result.TrySetResult(resource);
        }

        private void Cleanup()
        {
            foreach (var button in _buttons)
            {
                button.onClick -= HandleClick;
                Destroy(button.gameObject); 
            }
            _buttons.Clear();
        }
    }
}