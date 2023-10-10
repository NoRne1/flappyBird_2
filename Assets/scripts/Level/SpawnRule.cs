using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpawnRule : MonoBehaviour {
    public delegate void DestroyNotify();
    public event DestroyNotify DestroyedNotify;

    public Unit Monster;
    public float InitTime;
    public float Period;
    public int MaxNum;

    public int HP;
    //伤害
    public float attack;
    //护甲
    public float armor;
    //敏捷
    public float agile;
    //吸血
    public float hematophagia;


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
                enemy.attack = this.attack;
                enemy.armor = this.armor;
                enemy.agile = this.agile;
                enemy.hematophagia = this.hematophagia;
                enemy.OnDeath += Enemy_OnDeath;
                DestroyedNotify += enemy.OnDestroyed;
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

    public void OnDestroyed()
    {
        this.DestroyedNotify();
    }
}
