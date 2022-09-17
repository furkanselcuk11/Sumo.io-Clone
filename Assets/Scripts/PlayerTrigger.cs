using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{        
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yandýðýnda ses çalýþýr
            GameManager.gamemanagerInstance.RestartGame();  // Sahne yeniden baþlatýlýr
        }
    }
}
