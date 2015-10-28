using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

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
    private Text scoreUI;
    private Text timeUI;
    private Text outcomeUI;
    private enum States { Starting, Playing, Won, Crashed };
    private States state;
    private List<GameObject> capsules = new List<GameObject>();
    public GameObject ship;
    private Vector3 shipStartPos = new Vector3(-1.055f, 4.415375f, 0f);

    void Start()
    {
        scoreUI = GameObject.Find("Canvas/Score").GetComponent<Text>();
        timeUI = GameObject.Find("Canvas/Time").GetComponent<Text>();
        outcomeUI = GameObject.Find("Canvas/Outcome").GetComponent<Text>();
        outcomeUI.enabled = false;
        for(int i = 0; i < GameObject.Find("Level/Capsules").transform.childCount; i++)
        {
            capsules.Add(GameObject.Find("Level/Capsules").transform.GetChild(i).gameObject);
        }
        state = States.Starting;
    }

    void Update()
    {
        scoreUI.text = "Score: " + score.ToString();
        if(started)
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
            foreach (Renderer renderer in ship.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
            state = States.Crashed;
            StartCoroutine("Respawn");
        }
    }

    public void GetCapsule(GameObject capsule)
    {
        score++;
        capsule.SetActive(false);
        capsules.Remove(capsule);
        if(capsules.Count == 0)
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

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        outcomeUI.enabled = false;
        ship.transform.position = shipStartPos;
        ship.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
        alive = true;
        foreach (Renderer renderer in ship.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }
        state = States.Playing;
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(0);
    }
}