using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private float bumpAmonut; // Geri itme gücü

    // AI tirigger olayarý
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Eðer AI, Player karakterine çarpmýþsa Player karakteri addFoce iþlemi ile  geri itilir
            collision.gameObject.GetComponent<PlayerController>().enabled = false;  // PlayerController compenenti devre dýþý býrakýlýr
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>(); // Çarptýðý objenin Rigidbody compenentime eriþ
            playerRb.AddForce(this.transform.forward * (bumpAmonut*50), ForceMode.Impulse); // geri itme iþlemi gerçekleþtir 
            collision.gameObject.GetComponent<PlayerController>().enabled = true; // PlayerController compenenti aktif edilir
        }
    }    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            // OutOfPlay temas etmiþsse karakter yanar ve yok olur
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yandýðýnda ses çalýþýr
            GameManager.gamemanagerInstance.contestant.Remove(this.gameObject.transform);     // Karakter contestant listesinde silinir      
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Eðer  AI,AI karakterine çarpmýþsa AI karakteri addFoce iþlemi ile  geri itilir
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = false;// BasicEnemyAI compenenti devre dýþý býrakýlýr
            other.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = false;  // bu objenin EnemyIsPlayerShot compenenti içinde player vurdumu false olur
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>(); // Çarptýðý objenin Rigidbody compenentime eriþ
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);   // geri itme iþlemi gerçekleþtir  
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = true;   // BasicEnemyAI compenenti aktif edilir
        }
    }
    private void OnDisable()
    {
        if (this.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot)
        {
            // Eðer karakter ölürken EnemyIsPlayerShot içinde playerShot aktif sie yani karakteri Player düþürmüþ ise skor 1000 artar
            UIController.uiControllerInstance.score += 1000;
        }
    }
}
