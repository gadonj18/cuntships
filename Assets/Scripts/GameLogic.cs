using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private bool alive = true;
    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
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
    private int numCapsules;
    private Text scoreUI;
    private Text timeUI;
    private Text outcomeUI;
    private enum States { Starting, Playing, Won, Lost };
    private States state;

    void Start()
    {
        scoreUI = GameObject.Find("Canvas/Score").GetComponent<Text>();
        timeUI = GameObject.Find("Canvas/Time").GetComponent<Text>();
        outcomeUI = GameObject.Find("Canvas/Outcome").GetComponent<Text>();
        outcomeUI.enabled = false;
        numCapsules = GameObject.Find("Capsules").transform.childCount;
        state = States.Starting;
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
        if (state == States.Playing)
        {
            alive = false;
            outcomeUI.text = "You lose! ...bitch";
            outcomeUI.enabled = true;
            state = States.Lost;
            StartCoroutine("Restart");
        }
    }

    public void GetCapsule()
    {
        score++;
        numCapsules--;
        if(numCapsules == 0)
        {
            WinGame();
        }
    }

    public void StartGame()
    {
        if (state == States.Starting) {
            started = true;
            timeStart = Time.fixedTime;
            state = States.Playing;
       }
    }

    private void WinGame()
    {
        if (state == States.Playing)
        {
            started = false;
            outcomeUI.text = "You win! ...bitch";
            outcomeUI.enabled = true;
            state = States.Won;
            StartCoroutine("Restart");
        }
    }


    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(0);
    }
}