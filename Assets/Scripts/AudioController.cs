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
            thePlayer = FindObjectOfType<PlayerController>();   // Hiyerarþiden PlayerController ara ve thePlayer'a tanýmla
            s.source = gameObject.AddComponent<AudioSource>(); // AudioSource compenentini ekle
            s.source.clip = s.clip; // Ses kaynaðýna clip tanýmla
            s.source.volume = s.volume; // Ses kaynaðýna volume tanýmla
            s.source.loop = s.loop; // Ses kaynaðýna loop tanýmla
        }
    }

    public void Play(string name)
    {
        // name'den gelen  ses kaynaðýný çalýþtýr
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();    // Ses çalýþ
    }
    public void Stop(string name)
    {
        // name'den gelen  ses kaynaðýný durdur
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();    // Ses durdur
    }
}