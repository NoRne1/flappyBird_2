using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public Animator ani;

    void Start()
    {
        transform.localScale = new Vector3(2f, 2f, 1f);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void FadeIn(bool leftOrRight)
    {
        transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        if (leftOrRight)
        {
            this.ani.SetTrigger("Fade_in_left");
        }
        else
        {
            this.ani.SetTrigger("Fade_in_right");
        }
    }

    public void FadeOut(bool leftOrRight)
    {
        if (leftOrRight)
        {
            this.ani.SetTrigger("Fade_out_left");
        }
        else
        {
            this.ani.SetTrigger("Fade_out_right");
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}

