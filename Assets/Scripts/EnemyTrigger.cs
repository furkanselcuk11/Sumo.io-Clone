using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private float bumpAmonut; // Geri itme g�c�

    // AI tirigger olayar�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // E�er AI, Player karakterine �arpm��sa Player karakteri addFoce i�lemi ile  geri itilir
            collision.gameObject.GetComponent<PlayerController>().enabled = false;  // PlayerController compenenti devre d��� b�rak�l�r
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>(); // �arpt��� objenin Rigidbody compenentime eri�
            playerRb.AddForce(this.transform.forward * (bumpAmonut*50), ForceMode.Impulse); // geri itme i�lemi ger�ekle�tir 
            collision.gameObject.GetComponent<PlayerController>().enabled = true; // PlayerController compenenti aktif edilir
        }
    }    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            // OutOfPlay temas etmi�sse karakter yanar ve yok olur
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yand���nda ses �al���r
            GameManager.gamemanagerInstance.contestant.Remove(this.gameObject.transform);     // Karakter contestant listesinde silinir      
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // E�er  AI,AI karakterine �arpm��sa AI karakteri addFoce i�lemi ile  geri itilir
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = false;// BasicEnemyAI compenenti devre d��� b�rak�l�r
            other.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = false;  // bu objenin EnemyIsPlayerShot compenenti i�inde player vurdumu false olur
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>(); // �arpt��� objenin Rigidbody compenentime eri�
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);   // geri itme i�lemi ger�ekle�tir  
            other.gameObject.GetComponent<BasicEnemyAI>().enabled = true;   // BasicEnemyAI compenenti aktif edilir
        }
    }
    private void OnDisable()
    {
        if (this.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot)
        {
            // E�er karakter �l�rken EnemyIsPlayerShot i�inde playerShot aktif sie yani karakteri Player d���rm�� ise skor 1000 artar
            UIController.uiControllerInstance.score += 1000;
        }
    }
}
