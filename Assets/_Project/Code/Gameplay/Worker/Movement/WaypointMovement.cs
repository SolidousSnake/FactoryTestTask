using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Worker.Movement
{
    public class WaypointMovement
    {
        private readonly Transform _transform;
        private readonly IReadOnlyList<Transform> _waypoints;

        private float _speed;
        private int _currentWaypointIndex;

        public WaypointMovement(Transform transform, IReadOnlyList<Transform> waypoints, float speed = 0)
        {
            _transform = transform;
            _waypoints = waypoints;
            Reset();
            SetSpeed(speed);
        }

        public event Action ReachedFinalWaypoint;

        public Transform GetCurrentWayPoint() =>
            _currentWaypointIndex < _waypoints.Count ? _waypoints[_currentWaypointIndex] : null;

        public void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new Exception($"Speed must be positive. Received: {speed}");
            _speed = speed;
        }

        public void Move()
        {
            if (_currentWaypointIndex >= _waypoints.Count)
                return;

            Transform target = _waypoints[_currentWaypointIndex].transform;

            _transform.position = Vector3.MoveTowards(_transform.position
                , target.position, _speed * Time.deltaTime);

            if (Vector3.Distance(_transform.position, target.position) < 0.1f)
                _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Count)
                ReachedFinalWaypoint?.Invoke();
        }

        public void Reset() => _currentWaypointIndex = 0;
    }
}