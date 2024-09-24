using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicBtn : MonoBehaviour
{
    void Start()
    {
       // PlayerPrefs.SetInt("isMusic", isMusic ? 1 : 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TonggleMusic(){
       // isMusic = !isMusic;
        PlayerPrefs.SetInt("isMusic",0);

    }
}
