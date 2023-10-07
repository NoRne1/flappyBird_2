using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject uiReady;

    public GameObject uiInGame;

    public GameObject uiGameOver;

    public Slider hpbar;

    public Text uiLife;
    public Text uiLevelName;
    public Text uiLevelStartName;

    public GameObject uiLevelStart;
    public GameObject uiLevelEnd;

    // Use this for initialization
    void Start () {
        uiReady.SetActive(true);
        
    }


    public void UpdateLift(int score)
    {
        this.uiLife.text = score.ToString();
    }

    public void ShowLevelStart(string name)
    {
        this.uiLevelName.text = name;
        this.uiLevelStartName.text = name;
        
        uiLevelStart.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        this.hpbar.value = Mathf.Lerp(this.hpbar.value, Game.Instance.player.HP, 0.1f);
        if (Game.Instance.player != null)
            this.uiLife.text = Game.Instance.player.life.ToString();
    }

    public void UpdateUI()
    {
        uiReady.SetActive(Game.Instance.Status == GAME_STATUS.READY);
        uiInGame.SetActive(Game.Instance.Status == GAME_STATUS.INGAME);
        uiGameOver.SetActive(Game.Instance.Status == GAME_STATUS.OVER);
        this.hpbar.maxValue = Game.Instance.player.MaxHP;
        this.hpbar.value = Game.Instance.player.HP;
    }
}
