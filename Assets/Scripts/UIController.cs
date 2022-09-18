using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController uiControllerInstance;   // Diðer scriptler için eriþim saðlar  
    [Header("Texts")]
    [Space]
    [SerializeField] private GameObject gameFinishPanel;

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
    }
    void Update()
    {
        _contestantTxt.text = (GameManager.gamemanagerInstance.contestant.Count+1).ToString();
    }
    public void GameFinishPanel()
    {
        gameFinishPanel.SetActive(true);
    }
}
