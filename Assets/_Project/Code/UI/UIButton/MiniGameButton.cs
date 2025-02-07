using System;
using _Project.Code.Data.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class MiniGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _label;

        public event Action<ResourceType> onClick;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        public void Initialize(ResourceType type, string text)
        {
            _label.text = text;
            _button.onClick.AddListener(() => onClick?.Invoke(type));
        }
    }
}