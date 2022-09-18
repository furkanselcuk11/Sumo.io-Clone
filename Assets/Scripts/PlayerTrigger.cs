using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{   
    [SerializeField] private float bumpAmonut;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yandýðýnda ses çalýþýr
            GameManager.gamemanagerInstance.RestartGame();  // Sahne yeniden baþlatýlýr
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player - Enemy");
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = false;
            collision.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = true;
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = true;
        }
    }
}
