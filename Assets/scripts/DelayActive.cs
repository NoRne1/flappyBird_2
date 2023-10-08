using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour {

    public float dealy = 1;

	// Use this for initialization
	void OnEnable() {
        StartCoroutine(Delay());	
	}
	
	IEnumerator Delay()
    {
        yield return new WaitForSeconds(dealy);
        this.gameObject.SetActive(false);
    }
}
