using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public abstract class ARotation : MonoBehaviour
    {
        public float AngleSpeed = 150f;
        [Header("In game")]
        [SerializeField] protected float currentAngle;
        [SerializeField] protected float angleTo;

        public void SetDirection(Vector2 dir)
        {
            angleTo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        }

        public void SetAngle(float newAngle)
        {
            angleTo = newAngle;
        }

    }
}
