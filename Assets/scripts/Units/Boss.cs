using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public GameObject missileTemplate;

    public Transform firePoint2;

    public Transform firePoint3;

    public Transform battery;

    public Unit target;

    public float fireRate2 = 10f;
    float fireTimer2 = 0;

    public float UltCD = 10f;
    float fireTimer3 = 0;


    Missile missile = null;
    public override void OnStart()
    {
        this.Fly();
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        this.transform.position = new Vector3(15, 0, 0);
        yield return MoveTo(new Vector3(5, 0, 0));
        yield return NormalAttack();
    }

    IEnumerator NormalAttack()
    {
        while(true)
        {
            fireTimer2 += Time.deltaTime;
            Fire();
            Fire2();

            fireTimer3 += Time.deltaTime;
            if(fireTimer3 > UltCD)
            {
                yield return UltraAttack();
                fireTimer3 = 0;
            }
            yield return null;
        }
    }

    IEnumerator UltraAttack()
    {
        yield return MoveTo(new Vector3(5, 3, 0));
        yield return FireMissile();
        yield return MoveTo(new Vector3(5, 0, 0));
    }

    IEnumerator MoveTo(Vector3 pos)
    {
        while (true)
        {
            Vector3 dir  = (pos - this.transform.position);
            if (dir.magnitude < 0.1)
            {
                break;
            }
            this.transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FireMissile()
    {
        ani.SetTrigger("Skill");
        yield return new WaitForSeconds(3f);   
    }

    void Fire2()
    {
        if (fireTimer2 > 1f / fireRate2)
        {
            GameObject go = Instantiate(bulletTemplate, firePoint2.position, battery.rotation);
            Element bullent = go.GetComponent<Element>();
            bullent.direction = (target.transform.position - firePoint2.position).normalized;
            bullent.source = this;
            fireTimer2 = 0f;
        }
    }

    public void OnMissileLoad()
    {
        Debug.Log("OnMissileLoad");
        GameObject go = Instantiate(missileTemplate, firePoint3);
        missile = go.GetComponent<Missile>();
        missile.target = this.target.transform;
        missile.source = this;
    }

    public void OnMissileLaunch()
    {
        Debug.Log("OnMissileLaunch");
        if (missile == null)
            return;
        missile.transform.SetParent(null);
        missile.Launch();
    }

    public override void OnUpdate()
    {
        if(target!=null)
        {
            Vector3 dir = (target.transform.position - battery.position).normalized;
            battery.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Enemy:OnCollisionEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        // this.Die();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.death)
            return;
        Element bullet = col.gameObject.GetComponent<Element>();
        if (bullet == null)
        {
            return;
        }
        Debug.Log("Enemy:OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (bullet.side == SIDE.PLAYER)
        {
            this.Damage(bullet);
        }
    }
}
