using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
    [CreateAssetMenu(fileName = "Engine", menuName = "Part Data/Engine Data", order = 1)]
    public class EngineData : PartData
    {
        [Header("Parameter")]
        [SerializeField] private float _force;
        [SerializeField] private float _maximumSpeed;

        public float Force => _force;

        public float MaximumSpeed => _maximumSpeed;
    }
}
