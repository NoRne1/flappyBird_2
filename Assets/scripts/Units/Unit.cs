using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
    public FightTextManager fightTextManager;
    public SIDE side;

    public Rigidbody2D rigidbodyBird;

    public Animator ani;

    public float speed = 100f;
    public float fireRate = 10f;
    public float swordRate = 10f;

    protected bool death = false;

    public delegate void DeathNotify(Unit sender);
    public event DeathNotify OnDeath;

    public GameObject bulletTemplate;
    public Transform firePoint;

    protected Vector3 initPos;

    protected bool isFlying = false;

    //血量
    private float hp = 100f;

    public float HP
    {
        get { return this.hp; }
    }

    public float MaxHP = 100f;

    public Slider hpbar = null;

    //伤害
    public float attack;
    //护甲
    public float armor;
    //敏捷
    public float agile;
    //吸血
    public float hematophagia;
    //吸血数值
    public int hematophagiaValue = 5;

    float fireTimer = 0;
    float swordTimer = 0;

    public bool desoryOnDeath = false;

    // Use this for initialization
    void Start() {
        this.fightTextManager = this.GetComponent<FightTextManager>();
        if (fightTextManager != null)
        {
            fightTextManager.FightTextCanvas = GameObject.FindWithTag("FightTextCanvas").transform;
        }
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
        if (hpbar != null)
        {
            this.hpbar.value = Mathf.Lerp(this.hpbar.value, hp, 0.1f);
        }
        
        if (this.death)
            return;

        fireTimer += Time.deltaTime;
        swordTimer += Time.deltaTime;

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
        OnInit();
        if (hpbar != null)
        {
            this.hpbar.maxValue = this.MaxHP;
            this.hpbar.value = this.MaxHP;
        }
    }

    public virtual void OnInit()
    {

    }

    public void Fire()
    {
        if (fireTimer > 1f / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate, BulletManager.Instance.gameObject.transform);
            go.transform.position = firePoint.position;
            Element ele = go.GetComponent<Element>();
            ele.direction = this.side == SIDE.PLAYER ? Vector3.right : Vector3.left;
            ele.source = this;
            fireTimer = 0f;
        }
    }

    public void Sword()
    {
        if (swordTimer > 1f / swordRate)
        {
            this.ani.SetTrigger("Sword");
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
            Destroy(this.gameObject, 0.5f);
    }


    public void Damage(Element bullet)
    {
        this.Damage(GameUtil.Instance.CalcDamage(bullet.power, this.agile, bullet.source.attack, this.armor));
        bullet.source.InvokeHematophagia();
    }

    public void Damage(CloseArm arm)
    {
        this.Damage(GameUtil.Instance.CalcDamage(arm.power, this.agile, arm.source.attack, this.armor));
        arm.source.InvokeHematophagia();
    }

    public void Damage(int damage)
    {
        this.hp -= damage;
        fightTextManager.CreatFightText("-" + damage.ToString(), TextAnimationType.Burst, TextMoveType.RightParabola, transform);
        if (this.HP <= 0)
            this.Die();
    }

    public void InvokeHematophagia()
    {
        if (Random.Range(0f, 100f) < hematophagia)
        {
            AddHP(hematophagiaValue);
        }
    }

    public void AddHP(int hp)
    {
        this.hp += hp;
        fightTextManager.CreatFightText("+" + hp.ToString(), TextAnimationType.Normal, TextMoveType.RightParabola, transform);
        if (this.hp > MaxHP)
            this.hp = MaxHP;
    }
}
