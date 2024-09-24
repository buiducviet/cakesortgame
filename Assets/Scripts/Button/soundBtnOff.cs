using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundBtnOff : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
       // PlayerPrefs.SetInt("isSound", 0);
       /*  if(PlayerPrefs.GetInt("isSound",  0)==1){
            this.gameObject.SetActive(true);
        } */
        Debug.Log( PlayerPrefs.GetInt("isSound",  1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TonggleSound(){
        PlayerPrefs.SetInt("isSound", 1);

    }
}
