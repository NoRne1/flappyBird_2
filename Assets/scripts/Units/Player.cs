﻿/*
 Create By Ray : ray@raymix.net @ 极视教育
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public float invincibleTime = 3f;
    public float force = 100;
    private float timer = 0;
    // Update is called once per frame
    public override void OnUpdate()
    {
        if (this.death)
            return;

        timer += Time.deltaTime;


        //Vector2 pos = this.transform.position;
        //pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //this.transform.position = pos;


        //if (Input.GetButton("Fire1"))
        //{
        //    this.Fire();
        //}
        if (LifeManager.Instance.RemainLifes > 0 && Input.GetMouseButtonDown(1))
        {
            if(!isFlying && Game.Instance.Status == GAME_STATUS.INGAME)
            {
                //游戏中却在静止状态（刚重生）
                Fly();
            }
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
        if (LifeManager.Instance.RemainLifes > 0 && Input.GetMouseButtonDown(0))
        {
            if (!isFlying && Game.Instance.Status == GAME_STATUS.INGAME)
            {
                //游戏中却在静止状态（刚重生）
                Fly();
            }
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("GroundBoundary"))
        {
            this.Damage(Game.Instance.boundaryDamage);
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
    }
}
