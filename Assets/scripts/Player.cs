using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player: MonoBehaviour
{
    public Rigidbody2D bird;
    private Animator animator;
    public LifeManager lifeManager;
    public float force = 100;
    public Vector3 initPosition;

    public delegate void EarnScoreNotify();
    public event EarnScoreNotify EarnScore;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeManager.RemainLifes > 0 && Input.GetMouseButtonDown(0))
        {
            bird.velocity = Vector2.zero;
            bird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
    }

    public void Init()
    {
        transform.position = initPosition;
        lifeManager.Init();
    }

    public void Idle()
    {
        bird.simulated = false;
        animator.SetTrigger("idle");
    }

    public void Fly()
    {
        bird.simulated = true;
        animator.SetTrigger("fly");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Equals("spacing"))
        {
            Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("spacing"))
        {
            if (EarnScore != null)
            {
                EarnScore();
            }
        }
    }

    public void Death()
    {
        lifeManager.MinusLife();
    }
}
