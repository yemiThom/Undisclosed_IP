using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    float timeRemaining;
    public bool timerIsRunning = false;
     

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                GameManager.Instance.Lose();
            }
        }
    }

    public void StartTimer()
    {
        // set the timer 
        timeRemaining = GameManager.Instance.remainingLevelTime;
        // Starts the timer automatically
        timerIsRunning = true;
    }

    public void StopTimer(){
        // set bool to false
        timerIsRunning = false;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        UIManager.Instance.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
