using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject flight;

    private ArrayList obstacles;
    private float timer;
    private bool gameOver;
    private int count;
    public TextMeshProUGUI countTimer;
    public GameObject loseObject;
    public GameObject winObject;
    

    // Start is called before the first frame update
    void Start()
    {
        obstacles = new ArrayList();
        count = 2;
        loseObject.SetActive(false);
        winObject.SetActive(false);
        timer = 30f;
        gameOver = false;
        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (timer <= 0)
            {
                timer = 0;
                SetTimer();
                if (!winObject.activeSelf)
                {
                    loseObject.SetActive(true);
                    gameOver = true;
                    flight.SendMessage("Stop");
                }

            }
            if (winObject.activeSelf)
            {
                SetTimer();
            }

            if (!winObject.activeSelf && !loseObject.activeSelf)
            {
                timer -= 1 * Time.deltaTime;
                SetTimer();
            }
        }
    }

    public void Score( GameObject obstacle,GameObject sender)
    {
        
  

        if (obstacles.Count==0)
        {
            obstacles.Add(obstacle);
        }
        else if (!obstacles.Contains(obstacle)&& obstacles.Count != 3) {
            obstacles.Add(obstacle);
        }

        if (obstacles.Count == 3)
        {
            count++;
            timer = 30 - count * 5;
            SetTimer();
            obstacles.Clear();
            if (timer == 0)
            {
                sender.SendMessage("Stop");
                Win();
                gameOver = true;
            }
        }
    }

    public void Win()
    {
        if(!loseObject.activeSelf)
            winObject.SetActive(true);
    }

    public void GameOver()
    {
        loseObject.SetActive(true);
        flight.SendMessage("Stop");
    }

    void SetTimer()
    {
        countTimer.text = "Timer: " + timer.ToString();

    }
}
