using System.Collections;
using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;

namespace Aircraft
{
    public class BodyController : MonoBehaviour
    {
        [SerializeField] private float _armor;
        [SerializeField] private float _energyShield;

        public float Armor => _armor;

        public float EnergyShield => _energyShield;

        public void Setting(BodyData body)
        {
            //_armor = body.Armor;
            //_energyShield = body.Shield;
        }
    }
}
