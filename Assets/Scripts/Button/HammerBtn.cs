using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HammerBtn : MonoBehaviour
{
    public GameObject mainCamera;
    private Animator anim;
    public bool isHammer = false;
    public int hammerCount = 0;
    public TextMeshProUGUI text;
    private CoinController coinController;
    public Button hammerBtn;
    public GameObject mainCanvas;
    public GameObject hammerCanvas;
    public GameObject BGBooster;
    public GameObject icon;
    void Start()
    {
        coinController = FindObjectOfType<CoinController>();
        anim = mainCamera.GetComponent<Animator>();
        hammerBtn.onClick.AddListener(this.ClickEvent);
    }

    // Method to play the "usehammer" animation
    public void PlayHammerAnimation()
    {
        anim.Play("UseHammer");
    }
    public void HHammer(){
        isHammer = true;
    }
    public void UnAble(){
        isHammer = false;
    }

    /* public void increaseHammer(){
        hammerCount++;
    } */

    void ClickEvent(){
         if(coinController.coinAmount >= 100){
            hammerCount+=1;
            text.text = hammerCount+"";
            icon.SetActive(false);
            coinController.coinAmount-=100;
            coinController.text.text =  coinController.coinAmount+"";
        }
        else {
            Debug.Log("false");
        }
        if(hammerCount >=1){
            PlayHammerAnimation();
            HHammer();
            mainCanvas.SetActive(false);
            hammerCanvas.SetActive(true);
            BGBooster.SetActive(true);
        }
       
    }
}
