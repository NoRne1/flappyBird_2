using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit {

    public float lifetime = 4f;
    public Vector2 range;
    public ENEMY_TYPE enemyType;
    public int Score;
    public float initY = 0;

    // Use this for initialization
    public override void OnStart() {

        Destroy(this.gameObject, lifetime);
        initY = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(0, initY, 0);
        this.Fly();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        float y = 0;

        if (this.enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
        }

        this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * speed, initY + y);


        this.Fire();
    }

    public override void Die()
    {
        base.Die();
        Game.Instance.OnScore(Score);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.death)
            return;
        Element bullet = col.gameObject.GetComponent<Element>();
        if (bullet != null)
        {
            Debug.Log("Enemy OnTriggerEnter2D bullet: " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            if (bullet.side == SIDE.PLAYER)
            {
                this.Damage(bullet);
            }
            return;
        }
    }

    public void OnDestroyed()
    {
        if (this != null)
        {
            //死亡但不计分
            base.Die();
        }
    }
}
