using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;  // Di�er scriptler i�in eri�im sa�lar
    [SerializeField] private GameObject contestantNode;
    public List<Transform> contestant = new List<Transform>();
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
        for (int i = 0; i < contestantNode.transform.childCount; i++)
        {
            contestant.Add(contestantNode.transform.GetChild(i).gameObject.transform);
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
    public void StartGame()
    {
        AudioController.audioControllerInstance.Play("StartSound"); // Oyun ba�lad���nda ses �al���r        
        isStartGame = true;
        UIController.uiControllerInstance.gameStartTimerTxt.gameObject.SetActive(false);
    }
    void Update()
    {
        if (contestant.Count < 1)
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
        AudioController.audioControllerInstance.Play("FinishSound"); // Oyun sonu ses �al���r
        joystick.SetActive(false);  // joystick pasif hale gelir
        isFinish = true; // isFinish oyun snu aktif olur        
        UIController.uiControllerInstance.GameFinishPanel(); // FinishGame paneli a��l�r ve oyunu yeniden ba�lat�l�r
    }
}
