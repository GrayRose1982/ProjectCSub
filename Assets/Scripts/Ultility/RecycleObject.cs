using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject :MonoBehaviour, IPerUpdate
{
    [SerializeField] private float _timeStay;
    private float _timer;

    void OnDisable()
    {
        _timer = 0;
    }

    public void PerUpdate(float deltaTime)
    {
        _timer += Time.deltaTime;

        if (_timer > _timeStay)
        {
            transform.Recycle();
        }
    }
}
