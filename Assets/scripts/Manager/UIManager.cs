using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject uiReady;

    public GameObject uiInGame;

    public GameObject uiGameOver;

    public Slider hpbar;

    public Text uiLevelName;
    public Text uiLevelStartName;

    public Text scoreTextInGame;
    public TextMeshProUGUI scoreTextGameOver;
    public TextMeshProUGUI topScoreTextGameOver;

    public GameObject uiLevelStart;
    public GameObject uiLevelEnd;

    // Use this for initialization
    void Start () {
        uiReady.SetActive(true);
        Game.Instance.CurrentScore.AsObservable().DistinctUntilChanged<int>()
            .Subscribe(score =>
            {
                scoreTextInGame.text = score.ToString();
                scoreTextGameOver.text = score.ToString();
            });
        Game.Instance.TopScore.AsObservable().DistinctUntilChanged<int>()
            .Subscribe(score =>
            {
                topScoreTextGameOver.text = score.ToString();
            });
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
