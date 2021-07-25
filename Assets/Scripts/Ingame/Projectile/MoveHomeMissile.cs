using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class MoveHomeMissile : Movement
    {
        [SerializeField]private Transform _target;

        [SerializeField] private float _speedRotate;
        [SerializeField] private float _currentAngle;

        void OnEnable()
        {
            if(_target == null)
                return;

            var dirToTarget = _target.position - transform.localPosition;
            _currentAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
        }

        public override void PerUpdate(float deltaTime)
        {
            RotateToTarget(deltaTime);
            MoveToward(deltaTime);
        }

        private void RotateToTarget(float deltaTime)
        {
            if (_target == null)
                return;

            var dirToTarget= _target.position - transform.position;
            var angleToTarget = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
            _currentAngle = Mathf.MoveTowardsAngle(_currentAngle, angleToTarget, deltaTime * _speedRotate);

            transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            _direction = new Vector2(Mathf.Cos(_currentAngle * Mathf.Deg2Rad), Mathf.Sin(_currentAngle * Mathf.Deg2Rad));
        }
    }
}