using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateHelpController : MonoBehaviour
{
    private GameObject HammerAnim;
    private Animator animator;
    private GameObject hammer;
    private GameObject tong;
    private GameObject shuffle;

    void Start()
    {
        HammerAnim = GameObject.Find("AnimationPowerGroup");
        if(HammerAnim != null)
        {
            animator = HammerAnim.GetComponent<Animator>();
        }
        else 
        {
            Debug.Log("lỗi");
        }
        hammer = GameObject.Find("ButtonHammer");
        tong = GameObject.Find("ButtonSwitch");
    }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(this.gameObject.GetComponent<PlateController>().isPlaced == true && hammer.GetComponent<HammerBtn>().isHammer== true){
            if(animator != null)
            {
                HammerAnim.transform.position = transform.position;
                animator.Play("Break");
                this.gameObject.GetComponent<PlateController>().currentCake.animator.Play("disappear 1");
                this.gameObject.GetComponent<PlateController>().parentCircle.parentHole.SetActive(true);
                this.gameObject.GetComponent<PlateController>().parentCircle.dashedCircle.SetActive(false);
                this.gameObject.GetComponent<PlateController>().parentCircle.isFilled = false;
                hammer.GetComponent<HammerBtn>().hammerCount -=1;
                if( hammer.GetComponent<HammerBtn>().hammerCount==0){
                    hammer.GetComponent<HammerBtn>().text.text = "100";
                    hammer.GetComponent<HammerBtn>().icon.SetActive(true);
                }
                else{
                    hammer.GetComponent<HammerBtn>().text.text = hammer.GetComponent<HammerBtn>().hammerCount+"";
                }
            // hammer.GetComponent<HammerBtn>().isHammer = false;
            }
        }
        if(this.gameObject.GetComponent<PlateController>().isPlaced == true && tong.GetComponent<SwitchBtn>().isSwitch== true){
            this.gameObject.GetComponent<PlateController>().parentCircle.parentHole.SetActive(true);
            this.gameObject.GetComponent<PlateController>().parentCircle.dashedCircle.SetActive(false);
            this.gameObject.GetComponent<PlateController>().parentCircle.isFilled = false;
            this.gameObject.GetComponent<PlateController>().isPlaced = false;
            this.gameObject.GetComponent<PlateController>().OnMouseDown();
            tong.GetComponent<SwitchBtn>().switchCount -=1;
            
            if( tong.GetComponent<SwitchBtn>().switchCount==0){
                    tong.GetComponent<SwitchBtn>().text.text = "100";
                    tong.GetComponent<SwitchBtn>().icon.SetActive(true);
            }
            else {
                tong.GetComponent<SwitchBtn>().text.text = tong.GetComponent<SwitchBtn>().switchCount+"";
            }
        }
    }
}
