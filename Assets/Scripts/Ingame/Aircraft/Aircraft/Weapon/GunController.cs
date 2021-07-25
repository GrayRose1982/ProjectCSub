using System.Collections.Generic;
using ModelAirCraft;
using UnityEngine;


namespace Aircraft
{
    public class GunController : MonoBehaviour, IPerUpdate
    {
        [SerializeField] private List<IPerUpdate> _listWeapons;

        [SerializeField] private float _diffAngle;

        [Header("In game")]
        [SerializeField] private List<Transform>  _gunBarrels;
        [SerializeField] private string _layerTarget;

        //void Start()
        //{
        //    _listWeapons = new List<IPerUpdate>();
        //}

        public List<Transform> GunBarrel
        {
            set => _gunBarrels = value;
        }

        #region Setting data

        public void Setting(WeaponData data)
        {
            UpdateGunBarrel();

            _diffAngle = 135f;

            //TODO: Setting data for bullet? or create 
            //_layerTarget = 
        }

        #endregion

        #region Action

        public void AimTarget(Transform target)
        {
            _gunBarrels.ForEach(barrel => RotateToTarget(barrel, target));
        }

        public void PerUpdate(float deltaTime)
        {
            if(_listWeapons==null || _listWeapons.Count <= 0)
            {
                UpdateGunBarrel();
                return;
            }

            if (Input.GetKey(KeyCode.Space))
                foreach (var weapon in _listWeapons)
                    weapon.PerUpdate(deltaTime);
        }

        public void UpdateGunBarrel()
        {
            if (_listWeapons == null)
                _listWeapons = new List<IPerUpdate>();
            _listWeapons.Clear();

            foreach (var barrel in _gunBarrels)
                _listWeapons.Add(barrel.GetComponentInChildren<IPerUpdate>());
        }

        public void CreatePoolProjectile()
        {
            //_listWeapons..CreatePool(20);
        }

        private void RotateToTarget(Transform gunBarrel, Transform target)
        {
            if (target == null)
            {
                gunBarrel.localRotation = Quaternion.identity;
                return;
            }
            var dir = target.position - gunBarrel.position;
            var angleTo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
            var diffAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.z , angleTo);

            if (Mathf.Abs(diffAngle) < _diffAngle)
                gunBarrel.rotation = Quaternion.Euler(0, 0, angleTo);
            else gunBarrel.localRotation = Quaternion.identity;
        }
#endregion
    }
}
