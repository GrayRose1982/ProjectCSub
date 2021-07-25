using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;

namespace Aircraft
{
    public class EngineController : AMovement, IPerUpdate
    {
        public Rigidbody2D _rigid;

        [SerializeField] private float _force;
        [SerializeField] private float _maxSpeed;

        //public void PerUpdate(bool isActive)
        //{
        //    if (isActive)
        //    {
        //        direction = transform.up;
        //        _rigid.AddForce(transform.up * -speed);
        //    }
        //}

        public void PerUpdate(float deltaTime)
        {
            direction = _rigid.transform.up;
            _rigid.velocity = direction * -_maxSpeed;
            //_rigid.AddForce(direction * -_force);
            //var maxVelo = _rigid.velocity.magnitude;
            //if (maxVelo > _maxSpeed)
            //    _rigid.velocity = _rigid.velocity / maxVelo * _maxSpeed;
        }

        public void Setting(EngineData data)
        {
            _force = data.Force;
            _maxSpeed = data.MaximumSpeed;
        }
       
    }
}
