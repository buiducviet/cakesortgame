using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundBtn : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.SetInt("isSound", 1);
        /* if(PlayerPrefs.GetInt("isSound",  1)==1){
            this.gameObject.SetActive(true);
        } */
        Debug.Log( PlayerPrefs.GetInt("isSound",  1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TonggleSound(){
       // isSound = !isSound;
        PlayerPrefs.SetInt("isSound", 0);
    }
}
