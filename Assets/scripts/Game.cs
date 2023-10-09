using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UniRx;

public class Game : MonoSingleton<Game>
{
    public Player player;
    public float boundaryDamage;

    public BehaviorSubject<int> Coins = new BehaviorSubject<int>(0);
    public BehaviorSubject<int> Diamonds;
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
        Diamonds = new BehaviorSubject<int>(PlayerPrefs.GetInt("Diamonds", 0));
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
            LevelManager.Instance.DestroyLevel();
        }
    }


    public void StartGame()
    {
        Coins.OnNext(0);
        CurrentScore.OnNext(0);
        InitPlayer();
        LifeManager.Instance.Init();
        this.Status = GAME_STATUS.INGAME;
        Debug.LogFormat("StartGame:{0}", this.status);
        //player.Fly();
        LevelManager.Instance.LoadLevel(1);
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

    public void EarnCoins(int coins)
    {
        Coins.OnNext(Coins.Value + coins);
        Debug.LogFormat("EarnCoins! Total Coins:{0}", Coins.Value);
    }

    public void EarnDiamonds(int diamonds)
    {
        Diamonds.OnNext(Diamonds.Value + diamonds);
        PlayerPrefs.SetInt("Diamonds", Diamonds.Value);
        Debug.LogFormat("EarnDiamonds! Total Diamonds:{0}", Diamonds.Value);
    }
}
