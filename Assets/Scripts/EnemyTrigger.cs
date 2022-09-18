using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private float bumpAmonut;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.name + " -> " + collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerController>().enabled = false;
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(this.transform.forward * (bumpAmonut+50), ForceMode.Impulse);
            collision.gameObject.GetComponent<PlayerController>().enabled = true;
        }
    }    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yandýðýnda ses çalýþýr
            GameManager.gamemanagerInstance.contestant.Remove(this.gameObject.transform);            
            Destroy(this.gameObject,0.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(this.gameObject.name + " -> " + other.gameObject.name);
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = false;
            other.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = false;
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = true;
        }
    }
    private void OnDisable()
    {
        if (this.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot)
        {
            UIController.uiControllerInstance.score += 1000;
        }
    }
}
