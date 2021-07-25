using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
    [CreateAssetMenu(fileName = "Data", menuName = "Part Data/Armor Data", order = 2)]
    public class BodyData : PartData
    {
        [Header("Parameter")] [SerializeField] private float _armor;
        [SerializeField] private float _energyShield;

    }
}