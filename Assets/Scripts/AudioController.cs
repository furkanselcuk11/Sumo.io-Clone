using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioControllerInstance;

    public sound[] sounds;
    private PlayerController thePlayer;
    void Awake()
    {
        if (audioControllerInstance == null)
        {
            audioControllerInstance = this;
        }
        foreach (var s in sounds)
        {
            thePlayer = FindObjectOfType<PlayerController>();   // Hiyerar�iden PlayerController ara ve thePlayer'a tan�mla
            s.source = gameObject.AddComponent<AudioSource>(); // AudioSource compenentini ekle
            s.source.clip = s.clip; // Ses kayna��na clip tan�mla
            s.source.volume = s.volume; // Ses kayna��na volume tan�mla
            s.source.loop = s.loop; // Ses kayna��na loop tan�mla
        }
    }

    public void Play(string name)
    {
        // name'den gelen  ses kayna��n� �al��t�r
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();    // Ses �al��
    }
    public void Stop(string name)
    {
        // name'den gelen  ses kayna��n� durdur
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();    // Ses durdur
    }
}