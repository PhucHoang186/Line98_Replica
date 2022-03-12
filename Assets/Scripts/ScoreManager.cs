using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    int thisBestScore;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
        private void Start()
    {
        if(PlayerPrefs.HasKey("bestScore"))
        {
            UIManager.instance.bestScore = thisBestScore;
        }
        else
        {
            UIManager.instance.bestScore = 0;
        }
    }
    public void UpdateBestScore(int _bestScore)
    {
        thisBestScore = _bestScore;
        PlayerPrefs.SetInt("bestScore", thisBestScore);
    }
}
