using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : Unit
{
    public float invincibleTime = 3f;
    //力量
    public float force = 100;
    //飞行频率
    public float flyRate = 2;
    
    private float invincibleTimer = 0;
    private float flyTimer = 0;
    // Update is called once per frame
    public override void OnUpdate()
    {
        if (this.death)
            return;

        invincibleTimer += Time.deltaTime;
        flyTimer += Time.deltaTime;

        //Vector2 pos = this.transform.position;
        //pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //this.transform.position = pos;


        //if (Input.GetButton("Fire1"))
        //{
        //    this.Fire();
        //}
        if (LifeManager.Instance.RemainLifes > 0 && Input.GetMouseButtonDown(1) && flyTimer > 1f / flyRate)
        {
            if(!isFlying && Game.Instance.Status == GAME_STATUS.INGAME)
            {
                //游戏中却在静止状态（刚重生）
                Fly();
            }
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
            flyTimer = 0;
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

    public override void OnInit()
    {
        if (hpbar == null)
        {
            hpbar = GameObject.FindWithTag("PlayerHpBar")?.GetComponent<Slider>();
        }
    }

    public void Rebirth()
    {
        StartCoroutine(DoRebirth());
    }

    IEnumerator DoRebirth()
    {
        yield return new WaitForSeconds(2f);
        invincibleTimer = 0;
        this.Init();
    }

    public bool IsInvincible
    {
        get { return invincibleTimer < this.invincibleTime; }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;

        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (bullet == null && enemy == null)
        {
            return;
        }

        Debug.Log("Player:OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (bullet != null && bullet.side == SIDE.ENEMY)
        {
            this.Damage(bullet);
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
