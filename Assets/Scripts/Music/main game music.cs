using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maingamemusic : MonoBehaviour
{
    private static maingamemusic instance = null;
    private AudioSource audioSource;
    public AudioClip soundEffect;
    public AudioClip soundDisAppearEffect;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
