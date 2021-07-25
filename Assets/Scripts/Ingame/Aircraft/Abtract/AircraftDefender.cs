using System.Collections;
using System.Collections.Generic;
using Aircraft;
using UnityEngine;

public class AircraftDefender : MonoBehaviour, ITakeHittable
{
    public float HP;
    public void TakeHit(IHittable hit)
    {
        if (hit == null) return;

        HP -= hit.Damage;
        //Debug.Log($"Take hit {hit.Damage}",gameObject);
    }
}
