using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    private Animator anim;
    [Space]
    [Header("Player Controller")]
    [SerializeField] private float _movementSpeed = 300;
    [SerializeField] private float _rotationSpeed = 500;
    private bool isRunnig;

    [Space]
    [Header("Joystick Controller")]
    [SerializeField] Joystick joystick;   // Joystick scripti
    float vertical, horizontal; // Player yönü  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isRunnig)
        {
            anim.SetBool("isRunning", true);    // Run animasyonu oynar
        }
        else
        {
            anim.SetBool("isRunning", false);   // bekleme animasyonu oynar
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.gamemanagerInstance.isFinish && GameManager.gamemanagerInstance.isStartGame)
        {
            JoystickMove();   // Joystick kontrolü çalýþýr 
        }
        else
        {
            isRunnig = false;
        }
    }
    public void JoystickMove()
    {
        // Joystick kontrolü
        isRunnig = true;
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        transform.Translate(0, 0, _movementSpeed * Time.fixedDeltaTime); //  Oyun başladığında sürekli ileri hareket eder
        rb.velocity = new Vector3(horizontal, 0, vertical);              

        if (rb.velocity != Vector3.zero)
        {
            // Karakter dönüşü
            Quaternion temp = Quaternion.LookRotation(rb.velocity, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(transform.rotation, temp, _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}