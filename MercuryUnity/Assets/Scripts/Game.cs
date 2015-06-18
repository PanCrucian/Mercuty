using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public enum GameStates
    {
        Pause,
        Game
    }
    public GameStates gameState;

    public static Game Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Game _instance;

    public int score = 0;
    public int timer = 3;
    private int startTimer;
    public Text timerText;
    public Text scoreText;
    public Button restartBtn;
    public FloorRepeater floor0;
    public FloorRepeater floor1;

    void Awake()
    {
        _instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        startTimer = timer;
        RestartGame();
    }

    void Update()
    {
        if (timer == 0 && gameState == GameStates.Pause)
        {
            gameState = GameStates.Game;
            timerText.gameObject.SetActive(false);
        }
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        gameState = GameStates.Pause;
        foreach (Transform t in ObstacleController.Instance.transform)
            Destroy(t.gameObject);
        ObstacleController.Instance.AllowSpawn();
        Player.Instance.Restart();
        timer = startTimer;
        restartBtn.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        timerText.text = timer.ToString();
        StartCoroutine(TimerNumerator());
        floor0.index = 0;
        floor0.UpdatePosition();
        floor1.index = 1;
        floor1.UpdatePosition();
        score = 0;
    }

    public void GameOver()
    {
        gameState = GameStates.Pause;
        StartCoroutine(GameOverNumerator());
    }

    IEnumerator GameOverNumerator()
    {
        yield return new WaitForSeconds(0.75f);
        restartBtn.gameObject.SetActive(true);
    }

    IEnumerator TimerNumerator()
    {
        yield return new WaitForSeconds(0.5f);
        timer--;
        timerText.text = timer.ToString();
        if(timer > 0)
            StartCoroutine(TimerNumerator());
    }
}
