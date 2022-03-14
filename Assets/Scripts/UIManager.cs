using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    // score
    [HideInInspector] public int currentScore;
    [SerializeField] TMP_Text scoreText;
    //best score
    public int bestScore;
    [SerializeField] TMP_Text bestScoreText;
    //timer
    float timer;
    [SerializeField] TMP_Text timerText;
    //game menu
    [SerializeField] GameObject PauseMenu;
    //ball display
    [SerializeField] Image[] ballImages;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        UpdateAndDisplayScore(0);
        PauseMenu.SetActive(false);
    }
    void UpdateTimer()
    {
        timer += Time.deltaTime;
        string hours = Mathf.RoundToInt(timer / 3600).ToString("00");
        string minutes = Mathf.RoundToInt(timer / 60).ToString("00");
        string second = Mathf.RoundToInt((timer) % 60).ToString("00");
        timerText.text = hours+":"+minutes+":"+second;
    }
    private void Update()
    {
        UpdateTimer();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
        }
        else
        {
            bestScore = 100;
        }
        DisplayBestScore();
    }
   
    public void UpdateAndDisplayScore(int _score)// update score whenever the balls explode
    {
        currentScore += _score;
        scoreText.text = currentScore.ToString();
    }
    public void DisplayBestScore()//display best score when the game start
    {
        bestScoreText.text = bestScore.ToString();
    }
    public void UpdateBestScore(int _newbestScore)//update best score when the player lost
    {
        Debug.Log("Lose");
        if (_newbestScore > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", _newbestScore);
        }
    }
    private void OnEnable()
    {
        GamePlayManager.OnUpdateScoreGame += UpdateAndDisplayScore;
        GamePlayManager.OnLosingGame += UpdateBestScore;
        GamePlayManager.OnPauseGame += UpdatePauseMenu;

    }
    private void OnDisable()
    {
        GamePlayManager.OnUpdateScoreGame -= UpdateAndDisplayScore;
        GamePlayManager.OnLosingGame -= UpdateBestScore;
        GamePlayManager.OnPauseGame -= UpdatePauseMenu;
    }
    public void UpdatePauseMenu(GameState _currentState)
    {
        if(_currentState != GameState.Pause)
        {
            PauseMenu.SetActive(false);
        }
        else
        {
            PauseMenu.SetActive(true);
        }
    }
    public void DisplayBall(List<Ball> _ballList)
    {
        Debug.Log(_ballList.Count);
        if (_ballList.Count == 0)
            return;
        int i = 0;
        foreach (var ball in _ballList)
        {
            ballImages[i].sprite = ball.GetComponentInChildren<SpriteRenderer>().sprite;
            ballImages[i].color = ball.GetComponentInChildren<SpriteRenderer>().color;

            i++;
        }

    }
}