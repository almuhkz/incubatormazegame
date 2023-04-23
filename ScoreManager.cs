using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager instance;
    // Start is called before the first frame update
    public Text scoreText;
    [SerializeField] public Text timeText;
    public static int score = 0;
    public float currentTime = 0f;
    public static float startingTime = 30f;
    public static bool isPaused = false;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            startingTime = 180f;
        }
        else
        {
            startingTime = 30f;
        }
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime < 1)
        {
            timeText.color = Color.black;
            SceneManager.LoadScene(4);
        }
        if (currentTime < 4)
        {
            timeText.color = Color.red;
        }
        
        if (!isPaused)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        timeText.text = "TIME LEFT: " + currentTime.ToString("0");
        
    }

    public void AddPoint()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            score += 1 * Random.Range(1,3);
        }
        else
        {
            score += 1;
        }

        scoreText.text = "SCORE: " + score.ToString();
    }
}
