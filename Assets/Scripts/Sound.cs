using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sound
{
    [HideInInspector]
    public AudioSource source;  
    public string name; // Ses ismi
    public AudioClip clip;  // Ses kayna��
    [Range(0f, 1f)] // Ses volume y�ksekli�i ral���
    public float volume; // Ses volume
    public bool loop;   // Ses tekrar �al��s�n m�
}