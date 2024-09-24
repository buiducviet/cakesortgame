using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject audioSrc;
    private AudioSource audioSource;
    public AudioClip soundMax;
    public AudioClip sound;
    void Start()
    {
        audioSrc = GameObject.Find("Audio");

        // Lấy AudioSource từ cùng GameObject
        audioSource = audioSrc.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlaySoundMax(){
         if (audioSource != null && soundMax != null)
        {
            audioSource.PlayOneShot(soundMax);
        }

    }

    void PlaySound(){
          if (audioSource != null && sound!= null)
        {
            audioSource.PlayOneShot(sound);
        }

    }
}
