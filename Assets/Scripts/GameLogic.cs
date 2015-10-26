﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private bool alive = true;
    public bool Alive
    {
        get { return alive; }
    }
    private bool started = false;
    public bool Started
    {
        get { return started; }
    }
    private float timeStart;
    private int score;
    public int Score
    {
        get { return score; }
    }
    private Text scoreUI;
    private Text timeUI;

    void Start()
    {
        scoreUI = GameObject.Find("Canvas/Score").GetComponent<Text>();
        timeUI = GameObject.Find("Canvas/Time").GetComponent<Text>();
    }

    void Update()
    {
        scoreUI.text = "Score: " + score.ToString();
        if(started && alive)
        {
            int minutes = (int)((Time.fixedTime - timeStart) / 60);
            float seconds = (Time.fixedTime - timeStart) % 60;
            timeUI.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00.00");
        }
    }

    public void Crash()
    {
        alive = false;
    }

    public void GetCapsule()
    {
        score++;
    }
    
    public void StartGame()
    {
        started = true;
        timeStart = Time.fixedTime;
    }
}