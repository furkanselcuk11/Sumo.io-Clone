using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;  // Di�er scriptler i�in eri�im sa�lar
    public int contestantCount;    // Oyuncu Say�s�
    [SerializeField] private GameObject joystick;
    public bool isFinish;   // Oyunsonu
    public bool isStartGame;    // Oyun ba�lad�m�
    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    private void OnEnable()
    {
        Timer.OnTimerRunning += FinishGame; // OnTimerRunning eventi �al��t���nda zaman �al��acak fonksiyon
        Timer.OnTimerGameStart += StartGame; // OnTimerGameStart eventi �al��t���nda zaman �al��acak fonksiyon
    }
    private void OnDisable()
    {
        Timer.OnTimerRunning -= FinishGame; // OnTimerRunning eventi durdu�u zman fonk. �al��maz
        Timer.OnTimerGameStart -= StartGame; // OnTimerGameStart eventi durdu�u zman fonk. �al��maz
    }
    void Start()
    {        
        isStartGame = false;    // SAhne a��ld���nda oyun ba�lamas�n
    }
    void StartGame()
    {
        AudioController.audioControllerInstance.Play("StartSound"); // Oyun ba�lad���nda ses �al���r
        isStartGame = true;
        UIController.uiControllerInstance.gameStartTimerTxt.gameObject.SetActive(false);
    }
    void Update()
    {
        if (contestantCount <= 1)
        {
            FinishGame();   // Sahnede tek ki�i kalm��ssa oyunu bitir
        }
    }
    public void GamePausePlay(int value)
    {
        Time.timeScale = value;
        // Oyunu durdur - oynat
    }
    public void RestartGame()
    {   
        // Fonksiyon her �al��t���nda o andaki sanhe yeniden ba�lat�l�r
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void FinishGame()
    {
        joystick.SetActive(false);  // joystick pasif hale gelir
        isFinish = true; // isFinish oyun snu aktif olur
        AudioController.audioControllerInstance.Play("FinishSound"); // Oyun sonu ses �al���r
        UIController.uiControllerInstance.GameFinishPanel(); // FinishGame paneli a��l�r ve oyunu yeniden ba�lat�l�r
    }
}
