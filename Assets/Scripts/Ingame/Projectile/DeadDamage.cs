using System;
using System.Collections;
using System.Collections.Generic;
using Aircraft;
using DG.Tweening;
using UnityEngine;

namespace Projectile
{
    public class DeadDamage : MonoBehaviour, IHittable
    {
        [SerializeField] private float _size = .1f;
        [SerializeField] private float _damage = .1f;
        [SerializeField] private ParticleSystem TakeHit;
        public float Damage { get => _damage; set => _damage = value; }
        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        void Start()
        {
            TakeHit.CreatePool(5);
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position,_size);
        }
#endif

        void OnTriggerEnter2D(Collider2D hit)
        {
            var objTakeHit= hit.GetComponent<ITakeHittable>();
            var efx = TakeHit.Spawn(transform.position, Quaternion.identity);
            efx.Play(true);
            objTakeHit?.TakeHit(this);
            gameObject.Recycle();

            DOVirtual.DelayedCall(.2f, efx.Recycle);
        }
    }
}

