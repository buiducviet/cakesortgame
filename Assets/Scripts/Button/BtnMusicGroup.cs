using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMusicGroup : MonoBehaviour
{
    public GameObject btnMusicOn;
    public GameObject btnMusicOff;
    public GameObject gameMainMusic;
    void Start()
    {
        btnMusicOn.SetActive(PlayerPrefs.GetInt("isMusic", 1)==1);
        btnMusicOff.SetActive(PlayerPrefs.GetInt("isMusic", 1)==0);
        gameMainMusic.SetActive(PlayerPrefs.GetInt("isMusic", 1)==1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
