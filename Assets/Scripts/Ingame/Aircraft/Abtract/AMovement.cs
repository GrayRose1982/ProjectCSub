using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMovement : MonoBehaviour
{
    [SerializeField] protected Vector2 direction = Vector2.one;
    [SerializeField] protected float speed = 1f;


    public void SetMovement(Vector2 dir, float speed)
    {
        direction = dir;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x));
        this.speed = speed;
    }

    public void SetMovement(float direction, float speed)
    {
        this.direction = new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(0, 0, direction);
        this.speed = speed;
    }

    protected virtual void MoveToward(float deltaTime)
    {
        var curPos = (Vector2)transform.localPosition;
        curPos += direction * speed * deltaTime;
        transform.localPosition = curPos;
    }
}
