using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void OnTimerIsRunning();  // Timer Çalýþma
    public static event OnTimerIsRunning OnTimerRunning;   // Timer Çalýþma Eventi
    public delegate void OnTimerIsGameStart();  // Timer Çalýþma
    public static event OnTimerIsGameStart OnTimerGameStart;   // Timer Çalýþma Eventi

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
                // Oyuna baþlamak için 3 saniye beklenir
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
            // Eðer timerIsRunning true ise ve Oyunsonu olmamýþsa timer çalýþsýn
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimer(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerRunning();   // Eðer timerIsRunning false olmuþsa OnTimerRunning eventi çalýþýr
            }            
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60); // Dakikaya çevirir
        float seconds = Mathf.FloorToInt(currentTime % 60); // saniyeye çevirir
        UIController.uiControllerInstance.timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Ekranda dakike ve saniye biçimine gösterir
    }
}
