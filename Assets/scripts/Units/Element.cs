/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public float speed;

    public Vector3 direction = Vector3.zero;

    public SIDE side;

    public float power = 1;

    public float lifeTime;

    // Use this for initialization
    void Start() {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update() {

        OnUpdate();

    }

    public virtual void OnUpdate()
    {
        this.transform.position += speed * Time.deltaTime * direction;

        if (!GameUtil.Instance.InScreen(this.transform.position))
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
