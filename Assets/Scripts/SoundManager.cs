using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Use this for initialization
    public AudioClip[] sonidos;
    public AudioClip[] sonidosCombate;
    public AudioClip[] ataquesEnemigo;
    public AudioClip[] pasos;
    public AudioClip[] gemidosWeeb;
    public AudioClip[] ataquesWeeb;
    public AudioClip[] gemidosEnemy;
    public AudioClip[] ataquesEnemy;
    public AudioSource sourceSonidos;
    public AudioSource sourcePasos;
    public AudioSource sourceEnemigoStabbing;
    public AudioSource sourceExpendedora;
    public AudioSource sourceTuberia;

    public float volumeSource;
    public bool isWalking;

	void Start () {
        volumeSource = sourceSonidos.volume;


    }
	
	// Update is called once per frame
	void Update () {
        //if (isWalking && !sourcePasos.isPlaying)
        //{
        //    sourcePasos.pitch = Random.Range(0.9f, 1.1f);
        //    sourcePasos.Play();
        //}
		
	}

    public void PlaySound(int clip)
    {
        print("Entra en playSound con el clip: " + clip);
        sourceSonidos.PlayOneShot(sonidos[clip], volumeSource);
    }
    public void PlaySoundCombat(int clip)
    {
        print("Entra en playSoundCombat con el clip: " + clip);
        sourceSonidos.PlayOneShot(sonidosCombate[clip], volumeSource);
    }
    public void PlayRandomAttackEnemy()
    {
        int random = Random.Range(0, ataquesEnemigo.Length);
        sourceSonidos.PlayOneShot(ataquesEnemigo[random], volumeSource);
        print("Entra en PlayRandomAttackEnemy con el random: " + random);
    }
    public void PlayRandomAttackWeeb()
    {
        int random = Random.Range(0, ataquesWeeb.Length);
        sourceSonidos.PlayOneShot(ataquesWeeb[random], volumeSource);
        print("Entra en PlayRandomAttackWeeb con el random: " + random);
    }

    public void PlayRandomGemidoWeeb()
    {
        int random = Random.Range(0, gemidosWeeb.Length);
        sourceSonidos.PlayOneShot(gemidosWeeb[random], volumeSource);
        print("Entra en PlayRandomGemidoWeeb con el random: " + random);
    }
    public void PlayRandomGemidoEnemy()
    {
        int random = Random.Range(0, gemidosEnemy.Length);
        sourceSonidos.PlayOneShot(gemidosEnemy[random], volumeSource);
        print("Entra en PlayRandomGemidoEnemy con el random: " + random);
    }

    public void PlayStab()
    {
        sourceEnemigoStabbing.pitch = Random.Range(0.9f, 1.1f);
        sourceEnemigoStabbing.Play();
    }
    public void BotonTuberia()
    {
        sourceTuberia.pitch = Random.Range(0.9f, 1f);
        sourceTuberia.Play();
    }


    public void BotonExpendedora()
    {
        sourceExpendedora.pitch = Random.Range(0.9f, 1f);
        sourceExpendedora.Play();
    }

}
