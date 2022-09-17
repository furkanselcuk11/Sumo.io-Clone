using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;  // Diðer scriptler için eriþim saðlar
    public int contestantCount;    // Oyuncu Sayýsý
    [SerializeField] private GameObject joystick;
    public bool isFinish;   // Oyunsonu
    public bool isStartGame;    // Oyun baþladýmý
    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    private void OnEnable()
    {
        Timer.OnTimerRunning += FinishGame; // OnTimerRunning eventi çalýþtýðýnda zaman çalýþacak fonksiyon
        Timer.OnTimerGameStart += StartGame; // OnTimerGameStart eventi çalýþtýðýnda zaman çalýþacak fonksiyon
    }
    private void OnDisable()
    {
        Timer.OnTimerRunning -= FinishGame; // OnTimerRunning eventi durduðu zman fonk. çalýþmaz
        Timer.OnTimerGameStart -= StartGame; // OnTimerGameStart eventi durduðu zman fonk. çalýþmaz
    }
    void Start()
    {        
        isStartGame = false;    // SAhne açýldýðýnda oyun baþlamasýn
    }
    void StartGame()
    {
        AudioController.audioControllerInstance.Play("StartSound"); // Oyun baþladýðýnda ses çalýþýr
        isStartGame = true;
        UIController.uiControllerInstance.gameStartTimerTxt.gameObject.SetActive(false);
    }
    void Update()
    {
        if (contestantCount <= 1)
        {
            FinishGame();   // Sahnede tek kiþi kalmýþssa oyunu bitir
        }
    }
    public void GamePausePlay(int value)
    {
        Time.timeScale = value;
        // Oyunu durdur - oynat
    }
    public void RestartGame()
    {   
        // Fonksiyon her çalýþtýðýnda o andaki sanhe yeniden baþlatýlýr
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void FinishGame()
    {
        joystick.SetActive(false);  // joystick pasif hale gelir
        isFinish = true; // isFinish oyun snu aktif olur
        AudioController.audioControllerInstance.Play("FinishSound"); // Oyun sonu ses çalýþýr
        UIController.uiControllerInstance.GameFinishPanel(); // FinishGame paneli açýlýr ve oyunu yeniden baþlatýlýr
    }
}
