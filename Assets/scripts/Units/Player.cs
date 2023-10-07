/*
 Create By Ray : ray@raymix.net @ 极视教育
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public float invincibleTime = 3f;

    private float timer = 0;
    // Update is called once per frame
    public override void OnUpdate()
    {
        if (this.death)
            return;

        timer += Time.deltaTime;


        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.transform.position = pos;


        if (Input.GetButton("Fire1"))
        {
            this.Fire();
        }
    }

    public void Rebirth()
    {
        StartCoroutine(DoRebirth());
    }

    IEnumerator DoRebirth()
    {
        yield return new WaitForSeconds(2f);
        timer = 0;
        this.Init();
        this.Fly();

    }

    public bool IsInvincible
    {
        get { return timer < this.invincibleTime; }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;

        Item item = col.gameObject.GetComponent<Item>();
        if (item != null)
        {
            item.Use(this);
        }

        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (bullet == null && enemy == null)
        {
            return;
        }

        Debug.Log("Player:OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (bullet != null && bullet.side == SIDE.ENEMY)
        {
            this.Damage(bullet.power);
        }
        if (enemy != null)
        {
            this.Die();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;

        Debug.Log("Player:OnTriggerExit2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }
}
