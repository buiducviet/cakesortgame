using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrateBtn : MonoBehaviour
{
    void Start()
    {
       // PlayerPrefs.SetInt("isVibration", isVibration ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TonggleVibration(){
        //isVibration = !isVibration;
        // Save the updated setting in PlayerPrefs
        PlayerPrefs.SetInt("isVibration", 0);

    }
}
