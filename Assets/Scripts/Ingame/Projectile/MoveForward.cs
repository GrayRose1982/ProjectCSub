using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class MoveForward : Movement
    {
        public override void PerUpdate(float deltaTime)
        {
            MoveToward(deltaTime);
        }
    }
}
