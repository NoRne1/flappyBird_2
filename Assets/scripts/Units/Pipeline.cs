/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour {


    public Vector3 speed;

    public Vector2 range;

    public float lifeTime = 7;

	// Use this for initialization
	void Start () {
        this.Init();
	}

    float t = 0;

    public void Init()
    {
        float y = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(0, y, 0);
        

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += speed * Time.deltaTime;
        t += Time.deltaTime;
        if(t> lifeTime)
        {
            t = 0;
            this.Init();
        }

    }
}
