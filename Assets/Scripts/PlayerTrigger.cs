using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{   
    [SerializeField] private float bumpAmonut;  // Geri itme g�c�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfPlay"))
        {
            // OutOfPlay temas etmi�sse karakter yanar ve yok olur
            AudioController.audioControllerInstance.Play("DiedSound"); // Karakter yand���nda ses �al���r
            GameManager.gamemanagerInstance.RestartGame();  // Sahne yeniden ba�lat�l�r
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // E�er  Player,AI karakterine �arpm��sa AI karakteri addFoce i�lemi ile  geri itilir
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = false;  // BasicEnemyAI compenenti devre d��� b�rak�l�r
            collision.gameObject.GetComponent<EnemyIsPlayerShot>().playerShot = true;   // Player�n �arpt��� objenin EnemyIsPlayerShot compenenti i�indeki player vurdumu aktif olur
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>(); // �arpt��� objenin Rigidbody compenentime eri�
            enemyRb.AddForce(this.transform.forward * bumpAmonut, ForceMode.Impulse);       // geri itme i�lemi ger�ekle�tir   
            collision.gameObject.GetComponent<BasicEnemyAI>().enabled = true; // BasicEnemyAI compenenti aktif edilir
        }
    }
}
