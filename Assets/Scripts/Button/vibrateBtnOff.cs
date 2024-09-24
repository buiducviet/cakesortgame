using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrateBtnOff : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TonggleVibration(){
        PlayerPrefs.SetInt("isVibration", 1);

    }
}
