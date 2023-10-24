using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour {


    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();
    private List<SpawnRule> InstantiatedRules = new List<SpawnRule>();

    float timeSinceLevelStart = 0;

    float levelStartTime = 0;


    public float bossTime = 60f;

    float timer = 0;

    Boss boss = null;

    // Use this for initialization
    void Start () {
        StartCoroutine(RunLevel());
        levelStartTime = Time.realtimeSinceStartup;
    }

    IEnumerator RunLevel()
    {
        UIManager.Instance.ShowLevelStart(string.Format("LEVEL {0} {1}", this.LevelID, this.Name));
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Rules.Count; i++)
        {
            InstantiatedRules.Add(Instantiate<SpawnRule>(Rules[i], this.transform));
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;
        if (timeSinceLevelStart > bossTime)
        {
            if (boss == null)
            {
                boss = (Boss)UnitManager.Instance.CreateEnemy(this.Boss.gameObject);
                boss.target = Game.Instance.Player;
                boss.Fly();
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    private void Boss_OnDeath(Unit sender)
    {
        LevelManager.Instance.nextLevel();
    }

    public void OnDestroyed()
    {
        for (int i = 0; i < InstantiatedRules.Count; i++)
        {
            InstantiatedRules[i].OnDestroyed();
        }
        if (boss != null)
        {
            Destroy(boss.gameObject);
        }
        Destroy(this.gameObject);
    }
}
