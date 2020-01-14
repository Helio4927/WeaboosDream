using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSoundtrack : MonoBehaviour {

    // Use this for initialization
    public AudioClip[] ostSong;
    public AudioSource soundtrackSource;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaySoundtrackSong(int ostSongNumber)
    {

        StartCoroutine(ManagerSoundtrack.FadeOut(soundtrackSource, 1, ostSong[ostSongNumber]));
        Invoke("CallSong", 1);
    }

    public void CallSong()
    {
        StopAllCoroutines();
        StartCoroutine(ManagerSoundtrack.FadeIn(soundtrackSource, 1));
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, AudioClip nextSong)
    {
     
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
            if (audioSource.volume == 0)
            {
                audioSource.clip = nextSong;
            }
           
        }
        
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
