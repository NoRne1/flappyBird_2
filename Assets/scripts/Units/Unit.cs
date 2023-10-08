/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

    public SIDE side;

    public Rigidbody2D rigidbodyBird;

    public Animator ani;

    public float speed = 100f;
    public float fireRate = 10f;

    protected bool death = false;

    public delegate void DeathNotify(Unit sender);
    public event DeathNotify OnDeath;

    public GameObject bulletTemplate;
    public Transform firePoint;

    protected Vector3 initPos;

    protected bool isFlying = false;

    private float hp = 1000f;

    public float HP
    {
        get { return this.hp; }
    }

    public float MaxHP = 1000f;

    public float Attack;

    float fireTimer = 0;

    public bool desoryOnDeath = false;

    // Use this for initialization
    void Start() {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
        this.Init();
        OnStart();
}

    public virtual void OnStart()
    {

    }

    // Update is called once per frame
    void Update () {

        if (this.death)
            return;

        fireTimer += Time.deltaTime;

        OnUpdate();

    }

    public virtual void OnUpdate()
    {
        
    }


    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
        this.hp = this.MaxHP;
    }

    public void Fire()
    {
        if (fireTimer > 1f / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate, BulletManager.Instance.gameObject.transform);
            go.transform.position = firePoint.position;
            go.GetComponent<Element>().direction = this.side == SIDE.PLAYER ? Vector3.right : Vector3.left;
            fireTimer = 0f;
        }
    }

    public void Idle()
    {
        this.rigidbodyBird.simulated = false;
        this.ani.SetTrigger("Idle");
        this.isFlying = false;
    }

    public void Fly()
    {
        this.rigidbodyBird.simulated = true;
        this.ani.SetTrigger("Fly");
        this.isFlying = true;
    }

    public virtual void Die()
    {
        if (this.death)
            return;
        this.hp = 0;
        this.death = true;
        this.ani.SetTrigger("Die");
        if (this.OnDeath != null)
        {
            this.OnDeath(this);
        }

        if (desoryOnDeath)
            Destroy(this.gameObject, 0.2f);
    }


    public void Damage(float power)
    {
        Debug.Log("Unit:Damage power:" + power );
        this.hp -= power;

        if (this.HP <= 0)
            this.Die();
    }

    public void AddHP(int hp)
    {
        this.hp += hp;
        if (this.hp > MaxHP)
            this.hp = MaxHP;
    }
}
