/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UniRx;

public class Game : MonoSingleton<Game>
{
    public Player player;
    public int currentLevelId = 1;
    public float boundaryDamage;

    public BehaviorSubject<int> CurrentScore = new BehaviorSubject<int>(0);
    public BehaviorSubject<int> TopScore;
    GAME_STATUS status;

    public GAME_STATUS Status
    {
        get { return status; }
        set {
            this.status = value;
            UIManager.Instance.UpdateUI();
        }
    }
    private void Awake()
    {
        TopScore = new BehaviorSubject<int>(PlayerPrefs.GetInt("TopScore", 0));
    }
    // Use this for initialization
    void Start () {
        this.Status = GAME_STATUS.READY;
        this.player.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(Unit sender)
    {
        if (LifeManager.Instance.MinusLife())
        {
            player.Rebirth();
        }
        else
        {
            this.Status = GAME_STATUS.OVER;
            UnitManager.Instance.Clear();
        }
    }


    public void StartGame()
    {
        CurrentScore.OnNext(0);
        InitPlayer();
        LifeManager.Instance.Init();
        this.Status = GAME_STATUS.INGAME;
        Debug.LogFormat("StartGame:{0}", this.status);
        //player.Fly();
        LoadLevel();
    }

    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(this.currentLevelId);
        LevelManager.Instance.level.OnLevelEnd = OnLevelEnd;
    }

    void OnLevelEnd(Level.LEVEL_RESULT result)
    {
        if(result == Level.LEVEL_RESULT.SUCCESS)
        {
            this.currentLevelId++;
            this.LoadLevel();
        }
        else
        {
            this.Status = GAME_STATUS.OVER;
        }
    }

    public void Restart()
    {
        this.Status = GAME_STATUS.READY;
        // this.pipelineManager.Init();
        InitPlayer();
    }

    void InitPlayer()
    {
        this.player.Init();
    }

    public void OnScore(int score)
    {
        CurrentScore.OnNext(CurrentScore.Value + score);
        if (CurrentScore.Value > TopScore.Value)
        {
            TopScore.OnNext(CurrentScore.Value);
        }
        PlayerPrefs.SetInt("TopScore", TopScore.Value);
    }
}
