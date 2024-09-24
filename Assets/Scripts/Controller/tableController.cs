using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tableController:MonoBehaviour {
    public AudioClip sound;
    private GameObject audioSrc;
    private AudioSource audioSource;
    public void PlaySoundAdded()
    {
        //Debug.Log("PlaySoundAdded event triggered!");
        
       if (audioSource != null && sound != null && PlayerPrefs.GetInt("isSound", 1) == 1)
        {
            audioSource.PlayOneShot(sound);
        }
    }

     void Start()
    {
        audioSrc = GameObject.Find("Audio");

        // Lấy AudioSource từ cùng GameObject
        audioSource = audioSrc.GetComponent<AudioSource>();
    }
}