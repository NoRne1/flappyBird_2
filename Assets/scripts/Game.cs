using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class Game : MonoBehaviour
{
    public enum GameStatus
    {
        Ready,
        InGame,
        //Pause,
        GameOver
    }

    private GameStatus status;
    
    public GameStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            panelReady.SetActive(status == GameStatus.Ready);
            panelInGame.SetActive(status == GameStatus.InGame);
            //panelPause.SetActive(status == GameStatus.Pause);
            panelGameOver.SetActive(status == GameStatus.GameOver);
            Debug.LogFormat("game status: {0}", status);
        }
    }


    public GameObject panelReady;
    public GameObject panelInGame;
    //public GameObject panelPause;
    public GameObject panelGameOver;

    public TextMeshProUGUI scoreTextInGame;
    public TextMeshProUGUI scoreTextGameOver;
    public TextMeshProUGUI topScoreTextGameOver;

    public PipelineManager pipelineManager;
    public Player player;
    public Button MyButton;

    private int score = 0;
    private int topScore;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreTextInGame.text = score.ToString();
            scoreTextGameOver.text = score.ToString();
        }
    }

    // Use this for initialization
    void Start ()
    {
        player.lifeManager.IsAlive.AsObservable()
            .Where(isAlive => !isAlive)
            .Subscribe(isAlive => StopGame());
        player.EarnScore += EarnSocre;
        topScore = PlayerPrefs.GetInt("TopScore", 0);
        ReadyGame();
    }

    private void Update()
    {
        
    }

    private void EarnSocre()
    {
        Score += 1;
    }

    public void ReadyGame()
    {
        Status = GameStatus.Ready;
        score = 0;
        player.Init();
        player.Idle();
    }

    public void StartGame()
    {
        Status = GameStatus.InGame;
        pipelineManager.StartGenerate();
        player.Fly();
    }

    //public void pauseGame()
    //{
    //    Status = GameStatus.Pause;
    //    player.Idle();
    //}

    //public void resumeGame()
    //{
    //    Status = GameStatus.InGame;
    //    player.Fly();
    //}

    public void StopGame()
    {
        UpdateTopScore();
        Status = GameStatus.GameOver;
        pipelineManager.StopGenerate();
    }

    private void UpdateTopScore()
    {
        topScore = topScore > score ? topScore : score;
        PlayerPrefs.SetInt("TopScore", topScore);
        topScoreTextGameOver.text = topScore.ToString();
    }
}
