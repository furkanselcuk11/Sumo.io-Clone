using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIsPlayerShot : MonoBehaviour
{
    public bool playerShot; // AI oyunucusuna player vurdumu
    void Start()
    {
        this.playerShot = false;    // Ba�lang��ta false olarak ba�lar
    }   
}
