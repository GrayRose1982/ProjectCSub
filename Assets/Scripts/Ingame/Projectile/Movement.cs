using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Aircraft
{
    public abstract class Movement : MonoBehaviour, IPerUpdate
    {
        [SerializeField] protected Vector2 _direction = Vector2.one;
        [SerializeField] protected float _speed= 1f;

        [SerializeField] private IPerUpdate[] perUpdates;

        void Start()
        {
            perUpdates = GetComponents<IPerUpdate>();
        }

        //protected abstract void PerUpdate(float deltaTime);

        public void SetMovement(Vector2 dir, float speed)
        {
            _direction = dir;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(_direction.y,_direction.x));
            _speed = speed;
        }

        public void SetMovement(float direction, float speed)
        {
            _direction = new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));
            transform.rotation = Quaternion.Euler(0, 0, direction);
            _speed = speed;
        }

        protected virtual void MoveToward(float deltaTime)
        {
            var curPos = (Vector2)transform.localPosition;
            curPos += _direction * _speed * deltaTime;
            transform.localPosition = curPos;
        }

        public abstract void PerUpdate(float deltaTime);
    }
}
