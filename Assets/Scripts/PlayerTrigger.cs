using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{        
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yand���nda ses �al���r
            GameManager.gamemanagerInstance.RestartGame();  // Sahne yeniden ba�lat�l�r
        }
    }
}
