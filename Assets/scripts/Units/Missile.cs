using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element {

    // Use this for initialization

    public Transform target;

    private bool running = false;

    public GameObject fxExpold;


    public override void OnUpdate()
    {
        if (!running)
            return;

        if (target != null)
        {
            Vector3 dir = (target.position - this.transform.position);
            if(dir.magnitude  < 0.1)
            {
                this.Explod();
            }

            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            this.transform.position += speed * Time.deltaTime * dir.normalized;
        }
    }

    public void Launch()
    {
        running = true;

    }


    public void Explod()
    {
        Destroy(this.gameObject);
        Instantiate(fxExpold, this.transform.position, Quaternion.identity);

        if(target!=null)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(this);
        }
    }
}
