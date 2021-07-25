using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
    public enum BodyP { 
        Armor,
        EnergyShield,
    }

 
    public enum WingP
    {
        RotateSpeed,
        WindBlock,
    }

    public enum EngineP
    {
        MaxSpeed,
        Acceleration,
    }

    public enum WeaponP
    {
        Damage,
        Piece,

        FireRate,
    }

    public enum TypePart
    {
        Body,
        Wind,
        Engine,
        Gun,
        Missile
    }

    public enum TypeConnection
    {
        Root,
        Single,
        SymmetryLeft,
        SymmetryRight,
    }
}