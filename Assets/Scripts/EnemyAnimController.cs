using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator anim;
    private bool isRunnig;
    void Start()
    {
        this.anim = GetComponent<Animator>();
    }
    void Update()
    {
        AnimControler();
        if (!GameManager.gamemanagerInstance.isFinish && GameManager.gamemanagerInstance.isStartGame)
        {
            isRunnig = true;
        }
        else
        {
            isRunnig = false;
        }
    }
    private void AnimControler()
    {
        if (isRunnig)
        {
            this.anim.SetBool("isRunning", true);
        }
        else
        {
            this.anim.SetBool("isRunning", false);
        }
    }
}
