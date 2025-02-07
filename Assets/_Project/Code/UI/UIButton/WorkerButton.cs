using _Project.Code.Gameplay.Worker;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.UIButton
{
    [RequireComponent(typeof(Button))]
    public class WorkerButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        public void Initialize(WorkerFacade workerFacade) => _button.onClick.AddListener(workerFacade.GoToFactory);

        private void OnDestroy() => _button.onClick.RemoveAllListeners();
    }
}