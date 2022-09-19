using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController uiControllerInstance;   // Diðer scriptler için eriþim saðlar  
    [Header("Panel")]
    [Space]
    [SerializeField] private GameObject gameFinishPanel;
    [Space]
    [Header("Score")]
    public int score;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _timerTxt;
    [SerializeField] private TextMeshProUGUI _gameStartTimerTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _contestantTxt;

    public TextMeshProUGUI timerTxt
    {
        get { return _timerTxt; }
        set { _timerTxt = value; }
    }
    public TextMeshProUGUI gameStartTimerTxt
    {
        get { return _gameStartTimerTxt; }
        set { _gameStartTimerTxt = value; }
    }

    private void Awake()
    {
        if (uiControllerInstance == null)
        {
            uiControllerInstance = this;
        }
    }
    void Start()
    {
        gameFinishPanel.SetActive(false);
        score = 0;
        _scoreTxt.text = score.ToString();
    }
    void Update()
    {
        _contestantTxt.text = (GameManager.gamemanagerInstance.contestant.Count+1).ToString(); 
        // Oyundaki karakter sayýsýný contestant listesinden çeker ve üzerine player karakterini ekleyip ekranda yazdýrýr
        _scoreTxt.text = score.ToString();  // Skor yazýsný güncele
    }
    public void GameFinishPanel()
    {
        gameFinishPanel.SetActive(true);
    }
}
