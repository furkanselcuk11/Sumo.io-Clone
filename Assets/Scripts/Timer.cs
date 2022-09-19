using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void OnTimerIsRunning();  // Timer �al��ma
    public static event OnTimerIsRunning OnTimerRunning;   // Timer �al��ma Eventi
    public delegate void OnTimerIsGameStart();  // Timer �al��ma
    public static event OnTimerIsGameStart OnTimerGameStart;   // Timer �al��ma Eventi

    [SerializeField] private float timeRemaining = 59;
    [SerializeField] private float gameStartTimeRemaining = 3;
    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private bool gameStartTimerIsRunning;
    private void Start()
    {
        timerIsRunning = true;
        gameStartTimerIsRunning = true;
    }
    void Update()
    {
        if (gameStartTimerIsRunning && !GameManager.gamemanagerInstance.isStartGame)
        {
            if (gameStartTimeRemaining > 1)
            {
                // Oyuna ba�lamak i�in 3 saniye beklenir
                gameStartTimeRemaining -= Time.deltaTime;
                float seconds = Mathf.FloorToInt(gameStartTimeRemaining % 60);
                UIController.uiControllerInstance.gameStartTimerTxt.text = seconds.ToString();
            }
            else
            {
                gameStartTimeRemaining = 0;
                gameStartTimerIsRunning = false;
                OnTimerGameStart();
            }
        }
        

        if (timerIsRunning && !GameManager.gamemanagerInstance.isFinish && GameManager.gamemanagerInstance.isStartGame)
        {
            // E�er timerIsRunning true ise ve Oyunsonu olmam��sa timer �al��s�n
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimer(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerRunning();   // E�er timerIsRunning false olmu�sa OnTimerRunning eventi �al���r
            }            
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60); // Dakikaya �evirir
        float seconds = Mathf.FloorToInt(currentTime % 60); // saniyeye �evirir
        UIController.uiControllerInstance.timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Ekranda dakike ve saniye bi�imine g�sterir
    }
}
