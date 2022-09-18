using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;  // Diðer scriptler için eriþim saðlar
    [SerializeField] private GameObject contestantNode;
    public List<Transform> contestant = new List<Transform>();
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
        for (int i = 0; i < contestantNode.transform.childCount; i++)
        {
            contestant.Add(contestantNode.transform.GetChild(i).gameObject.transform);
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
    public void StartGame()
    {
        AudioController.audioControllerInstance.Play("StartSound"); // Oyun baþladýðýnda ses çalýþýr        
        isStartGame = true;
        UIController.uiControllerInstance.gameStartTimerTxt.gameObject.SetActive(false);
    }
    void Update()
    {
        if (contestant.Count < 1)
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
        AudioController.audioControllerInstance.Play("FinishSound"); // Oyun sonu ses çalýþýr
        joystick.SetActive(false);  // joystick pasif hale gelir
        isFinish = true; // isFinish oyun snu aktif olur        
        UIController.uiControllerInstance.GameFinishPanel(); // FinishGame paneli açýlýr ve oyunu yeniden baþlatýlýr
    }
}
