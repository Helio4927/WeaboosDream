using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auxiliarsonido : MonoBehaviour {
    public GameObject soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>().gameObject;
    }
    public void correrAudio()
    {
        GetComponent<AudioSource>().Play();      
    }
    public void llamarStab()
    {
        soundManager.GetComponent<SoundManager>().PlayStab();
    }
}
