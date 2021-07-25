using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Aircraft
{
    public class BulletShoot : MonoBehaviour, IPerUpdate
    {
        [Header("Data")]
        [SerializeField] private Movement _projectile;
        [SerializeField] private float _fireRate = .1f;
        [SerializeField] private Vector2Int _numberBullet = Vector2Int.one;
        [SerializeField] private float _angleDiff;
        [SerializeField] private string _layerTarget;
        [SerializeField] private float _timePerBullet;

        [Header("In game")]
        [SerializeField] private float _timer = 0;

        void Update()
        {
            PerUpdate(Time.deltaTime);
        }

        public void PerUpdate(float deltaTime)
        {
            var curTime = Time.time;
            if (_timer < curTime)
            {
                StartCoroutine(Fire());

                _timer = curTime + _fireRate;
            }
        }

        private IEnumerator Fire()
        {
            var numberBullet = GetNumberBullet();
            for (int i = 0; i < numberBullet; ++i)
            {
                var bullet = _projectile.Spawn(transform.position, Quaternion.identity);
                bullet.gameObject.layer = LayerMask.NameToLayer(_layerTarget);
                var angleMore = Random.Range(-_angleDiff, _angleDiff);
                bullet.SetMovement(transform.rotation.eulerAngles.z - 90+ angleMore, 30f);

                if(_timePerBullet > 0)
                    yield return new WaitForSeconds(_timePerBullet);
            }
        }

        private int GetNumberBullet()
        {
            return _numberBullet.x < 0 || _numberBullet.y < 0 ? 1 :
                _numberBullet.x == _numberBullet.y ? _numberBullet.x :
                Random.Range(_numberBullet.x, _numberBullet.y);
        }

        public void SetLayerTarget(string target)
        {
            _layerTarget = target;
        }
    }
}