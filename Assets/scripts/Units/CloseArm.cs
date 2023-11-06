using UnityEngine;
using System.Collections;
using Unity.Burst.Intrinsics;

public class CloseArm : MonoBehaviour
{
    public Unit source;

    public SIDE side;

    public float power = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (bullet == null && enemy == null)
        {
            return;
        }

        Debug.Log("Player:OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (bullet != null && bullet.side == SIDE.ENEMY)
        {
            bullet.DestroySelf();
        }
        if (enemy != null)
        {
            enemy.Damage(this);
        }
    }
}

