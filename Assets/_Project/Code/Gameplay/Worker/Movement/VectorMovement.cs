using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Worker.Movement
{
    public class VectorMovement
    {
        private readonly Transform _transform;
        private Transform _target;
        private float _speed;

        public VectorMovement(Transform transform, Transform target = null, float speed = 0)
        {
            _transform = transform;
            SetTarget(target);
            SetSpeed(speed);
        }

        public event Action Reached;

        public void SetTarget(Transform target) => _target = target;

        public void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new Exception($"Speed must be positive. Received: {speed}");
            _speed = speed;
        }

        public void Move()
        {
            _transform.position = Vector3.MoveTowards(_transform.position
                , _target.position, _speed * Time.deltaTime);

            if (Vector3.Distance(_transform.position, _target.position) < 0.1f)
                Reached?.Invoke();
        }
    }
}