/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour {


    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    public UnityAction<LEVEL_RESULT> OnLevelEnd;

    float timeSinceLevelStart = 0;

    float levelStartTime = 0;


    public float bossTime = 60f;

    float timer = 0;

    Boss boss = null;

    public enum LEVEL_RESULT
    {
        NONE,
        SUCCESS,
        FAILD
    }

    public LEVEL_RESULT result = LEVEL_RESULT.NONE;

    // Use this for initialization
    void Start () {

        StartCoroutine(RunLevel());

    }

    IEnumerator RunLevel()
    {
        UIManager.Instance.ShowLevelStart(string.Format("LEVEL {0} {1}", this.LevelID, this.Name));
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i], this.transform);
        }

    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        if (this.result != LEVEL_RESULT.NONE)
            return;

        if (timeSinceLevelStart > bossTime)
        {
            if (boss == null)
            {
                boss = (Boss)UnitManager.Instance.CreateEnemy(this.Boss.gameObject);
                boss.target = Game.Instance.player;
                boss.Fly();
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    private void Boss_OnDeath(Unit sender)
    {
        this.result = LEVEL_RESULT.SUCCESS;
        if (this.OnLevelEnd != null)
            this.OnLevelEnd(this.result);
    }
}
