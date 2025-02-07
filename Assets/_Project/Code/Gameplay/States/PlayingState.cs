using _Project.Code.Core.Utils;
using _Project.Code.Runtime.Core.StateMachine;
using _Project.Code.UI.View.States;
using UnityEngine;

namespace _Project.Code.Gameplay.States
{
    public class PlayingState : IState
    {
        private readonly PlayingStateView _stateView;

        public PlayingState(PlayingStateView stateView)
        {
            _stateView = stateView;
        }

        public void Enter()
        {
            _stateView.gameObject.SetActive(true);
            Time.timeScale = Constants.Time.ResumedValue;
        }

        public void Exit() => _stateView.gameObject.SetActive(false);
    }
}