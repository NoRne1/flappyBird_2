/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoSingleton<Game>
{
    public Player player;
    public int currentLevelId = 1;

    GAME_STATUS status;

    public GAME_STATUS Status
    {
        get { return status; }
        set {
            this.status = value;
            UIManager.Instance.UpdateUI();
        }
    }

    // Use this for initialization
    void Start () {
       
        this.Status = GAME_STATUS.READY;
        this.player.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(Unit sender)
    {
        if (player.life <= 0)
        {
            this.Status = GAME_STATUS.OVER;
            UnitManager.Instance.Clear();
        }
        else
        {
            player.Rebirth();
        }
    }


    public void StartGame()
    {
        InitPlayer();
        this.Status = GAME_STATUS.INGAME;
        Debug.LogFormat("StartGame:{0}", this.status);
        player.Fly();
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
}
