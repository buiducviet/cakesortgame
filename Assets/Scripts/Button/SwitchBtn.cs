using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    public GameObject mainCamera;
    private Animator anim;
    public bool isSwitch = false;
    public int switchCount = 0;
    public Button switchBtn;
    public GameObject mainCanvas;
    public GameObject switchCanvas;
    public GameObject BGBooster;
    public TextMeshProUGUI text;
    private CoinController coinController;
    public GameObject icon;
    

    void Start()
    {
        anim = mainCamera.GetComponent<Animator>();
        coinController = FindObjectOfType<CoinController>();
        switchBtn.onClick.AddListener(this.ClickEvent);
    }

    // Method to play the "usehammer" animation
    public void PlayHammerAnimation()
    {
        anim.Play("UseHammer");
    }
    public void SSwitch(){
        isSwitch = true;
    }
    public void UnAble(){
        isSwitch = false;
    }

    public void IncreaseSwitch(){
        switchCount++;
    }
      void ClickEvent(){
        if(coinController.coinAmount >= 100){
            switchCount+=1;
            text.text = switchCount+"";
            icon.SetActive(false);
            coinController.coinAmount-=100;
            coinController.text.text =  coinController.coinAmount+"";
        }
        else {
            Debug.Log("false");
        }
        if(switchCount >=1){
           
            PlayHammerAnimation();
            SSwitch();
            mainCanvas.SetActive(false);
            switchCanvas.SetActive(true);
            BGBooster.SetActive(true);
        }
        
    }
}
