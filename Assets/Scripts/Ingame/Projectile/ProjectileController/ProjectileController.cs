using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class ProjectileController : MonoBehaviour
    {
        private IPerUpdate[] perupdates;
        protected virtual void Start()
        {
            perupdates = GetComponents<IPerUpdate>();
        }

        protected virtual void Update()
        {
            var delta = Time.deltaTime;

            foreach (var update in perupdates)
                update.PerUpdate(delta);
        }
    }
}
