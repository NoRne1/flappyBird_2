using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnRule : MonoBehaviour {
    public Unit Monster;
    public float InitTime;
    public float Period;
    public int MaxNum;

    public int HP;
    public int Attack;


    float timeSinceLevelStart = 0;

    float levelStartTime = 0;

    int num = 0;
    float timer = 0;

    public List<ItemDropRuleStruct> RuleStructs = new List<ItemDropRuleStruct>();
    private List<ItemDropRule> Rules = new List<ItemDropRule>();

    // Use this for initialization
    void Start () {
        this.levelStartTime = Time.realtimeSinceStartup;
        for (int i = 0; i < RuleStructs.Count; i++)
        {
            Rules.Add(new ItemDropRule(RuleStructs[i]));
        }
    }
	


	// Update is called once per frame
	void Update () {

        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        if (num >= MaxNum) return;

        if (timeSinceLevelStart > InitTime)
        {//开始刷怪
            timer += Time.deltaTime;

            if(timer > Period)
            {
                timer = 0;
                Enemy enemy = UnitManager.Instance.CreateEnemy(this.Monster.gameObject);
                enemy.MaxHP = this.HP;
                enemy.Attack = this.Attack;
                enemy.OnDeath += Enemy_OnDeath;
                num++;
            }
        }
    }

    private void Enemy_OnDeath(Unit sender)
    {
        for (int i = 0; i < Rules.Count; i++)
        {
            Rules[i].Execute(sender.transform.position);
        }
    }
}
