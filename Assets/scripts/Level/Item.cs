using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour {
    public int worth;
	public float speed;
	public CurrencyType type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = (Game.Instance.Player.transform.position - transform.position).normalized;
        this.transform.position += dir * speed * Time.deltaTime;
	}


    public void Use()
    {
		switch (type)
		{
			case CurrencyType.Coin:
                Game.Instance.EarnCoins(worth);
				break;
            case CurrencyType.Diamond:
                Game.Instance.EarnDiamonds(worth);
                break;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Use();
        }
    }
}
public enum CurrencyType: int
{
	Coin = 1,
	Diamond = 2
}
