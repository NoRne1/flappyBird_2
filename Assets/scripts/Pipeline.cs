using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed;
    //public float destroyDalay;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyDalay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0);
    }
}
