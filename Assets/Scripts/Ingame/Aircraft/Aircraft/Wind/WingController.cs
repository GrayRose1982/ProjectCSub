using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;

namespace Aircraft
{
    public class WingController : ARotation, IPerUpdate
    {
        public Rigidbody2D Rigid;

        public void Setting(WingData data)
        {
            AngleSpeed = data.RotateSpeed;
        }

        public void PerUpdate(float deltaTime)
        {
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, angleTo, deltaTime * AngleSpeed);

            Rigid.rotation = currentAngle;
        }
    }
}
