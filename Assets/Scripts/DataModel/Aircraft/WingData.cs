using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelAirCraft
{
    [CreateAssetMenu(fileName = "Wing", menuName = "Part Data/Wing Data", order = 1)]
    public class WingData: PartData
    {
        [Header("Data")]
        [SerializeField] private float _rotateSpeed;

        public float RotateSpeed => _rotateSpeed;
    }
}
