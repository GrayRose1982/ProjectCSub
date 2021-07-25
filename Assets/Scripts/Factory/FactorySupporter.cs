using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class FactorySupporter : MonoBehaviour
    {
        public Factory f;

        public Factory F => f ?? (f = GetComponent<Factory>());

        void Reset()
        {
            f = GetComponent<Factory>();
        }
    }


}
