using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LifeManager : MonoBehaviour
{
    public int lifeCount;
    public GameObject lifeTemplate;
    public float spacing;
    private int remainLifes;
    public int RemainLifes
    {
        get { return remainLifes; }
        set {
            remainLifes = value;
            IsAlive.OnNext(remainLifes > 0);
        }
    }
    public Subject<bool> IsAlive = new Subject<bool>();
    private GameObject[] lifes;
    private bool isFirstInit = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        RemainLifes = lifeCount;
        if (isFirstInit)
        {
            GenerateLifes();
            isFirstInit = false;
        }
        else
        {
            for (int i = 0; i < lifeCount; i = i + 1)
            {
                lifes[i].SetActive(true);
            }
        }
    }
    private void GenerateLifes()
    {
        lifes = new GameObject[lifeCount];
        for (int i = 0; i < lifeCount; i = i + 1)
        {
            lifes[i] = Instantiate(lifeTemplate, transform);
            lifes[i].transform.position = new Vector3(transform.position.x + spacing * i,
                transform.position.y, transform.position.z);
        }
    }

    public bool MinusLife()
    {
        if (RemainLifes > 1)
        {
            RemainLifes--;
            lifes[RemainLifes].SetActive(false);
            return true;
        } else if (RemainLifes == 1)
        {
            RemainLifes--;
            lifes[RemainLifes].SetActive(false);
            return false;
        } else
        {
            Debug.Log("死的不能再死了！");
            return false;
        }
    }
}
