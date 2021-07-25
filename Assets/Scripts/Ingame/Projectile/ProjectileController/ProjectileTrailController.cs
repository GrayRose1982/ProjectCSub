using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class ProjectileTrailController : ProjectileController
    {
        [SerializeField] private TrailRenderer _trail;

        void OnDisable()
        {
            _trail.Clear();
        }
    }
}
