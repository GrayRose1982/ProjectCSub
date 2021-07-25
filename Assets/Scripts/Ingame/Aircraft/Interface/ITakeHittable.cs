using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public interface ITakeHittable
    {
        void TakeHit(IHittable hit);
    }
}
