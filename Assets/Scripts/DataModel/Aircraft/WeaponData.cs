using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
     [CreateAssetMenu(fileName = "Weapon", menuName = "Part Data/Weapon Data", order = 1)]
    public class WeaponData : PartData
    {
        [Header("Data")]
        [SerializeField] private string _gunBarrels;
        [SerializeField] private float _damage;

        [Header("Angle")]
        [SerializeField] private float _fireRate;
        [SerializeField] private float _diffAngle;
        //[SerializeField] private float _gunDiffAngle;

        [Header("Type")]
        [SerializeField] private TypeWeapon _type;

        [Header("Number")]
        [Tooltip("Number bullet from to")]
        [SerializeField] private Vector2Int _number = Vector2Int.one;

        public float FireRate => _fireRate;

        public float DiffAngle => _diffAngle;

        public string GunBarrels
        {
            get { return _gunBarrels; }
            set { _gunBarrels = value; }
        }
    }

    public enum TypeWeapon
    {
        Normal,
        Splash,
        Random,
    }
}
