using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnVibrateGroup : MonoBehaviour
{
    public GameObject btnVibrationOn;
    public GameObject btnVibrationOff;
    // Start is called before the first frame update
    void Start()
    {
        btnVibrationOn.SetActive(PlayerPrefs.GetInt("isVibration", 1)==1);
        btnVibrationOff.SetActive(PlayerPrefs.GetInt("isVibration", 1)==0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}