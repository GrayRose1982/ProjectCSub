using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public interface IHittable
    {
        float Damage { get; set; }

        void SetDamage(float damage);
    }
}