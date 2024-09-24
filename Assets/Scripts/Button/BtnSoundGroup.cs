using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSoundGroup : MonoBehaviour
{
    public GameObject btnSoundOn;
    public GameObject btnSoundOff;
    // Start is called before the first frame update
    void Start()
    {
        btnSoundOn.SetActive(PlayerPrefs.GetInt("isSound", 1)==1);
        btnSoundOff.SetActive(PlayerPrefs.GetInt("isSound", 1)==0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
