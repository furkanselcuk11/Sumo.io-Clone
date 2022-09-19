using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{   
    [SerializeField] private float bumpAmonut;  // Geri itme gücü
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            // OutOfPlay temas etmiþsse karakter yanar ve yok olur
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yandýðýnda ses çalýþýr
            GameManager.gamemanagerInstance.RestartGame();  // Sahne yeniden baþlatýlýr
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Eðer  Player,AI karakterine çarpmýþsa AI karakteri addFoce iþlemi ile  geri itilir
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = false;  // BasicEnemyAI compenenti devre dýþý býrakýlýr
            collision.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = true;   // Playerýn çarptýðý objenin EnemyIsPlayerShot compenenti içindeki player vurdumu aktif olur
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>(); // Çarptýðý objenin Rigidbody compenentime eriþ
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);       // geri itme iþlemi gerçekleþtir   
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = true; // BasicEnemyAI compenenti aktif edilir
        }
    }
}
