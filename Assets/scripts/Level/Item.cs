using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int AddHP = 50;

    public GameObject bullet;

    public float lifeTime = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
	}


    public void Use(Unit target)
    {
        target.AddHP(this.AddHP);
        Destroy(this.gameObject);
    }
}
