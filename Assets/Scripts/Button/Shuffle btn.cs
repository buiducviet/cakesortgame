using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shufflebtn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Slots;
    public GameObject SpawnCake;
    private PlateController plate1;
    private PlateController plate2;
    private PlateController plate3;
    private int shuffleCount = 0;
    public Button shuffleBtn;
    private CoinController coinController;
    public TextMeshProUGUI text;
    public GameObject icon;

    void Start()
    {
        // Lấy component PlateController của con của Slots[0]
        coinController = FindObjectOfType<CoinController>();
        shuffleBtn.onClick.AddListener(this.ClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ClickEvent(){
        if(shuffleCount >=1){
            Shuffle();
        }
        else{
            if(coinController.coinAmount >= 100){
                shuffleCount+=1;
                text.text = shuffleCount+"";
                icon.SetActive(false);
                coinController.coinAmount-=100;
                coinController.text.text =  coinController.coinAmount+"";
            }
            else {
                Debug.Log("false");
            }

        }
        
       
       
    }

    public void Shuffle()
    {
        plate1 = Slots[0].GetComponentInChildren<PlateController>();
        plate2 = Slots[1].GetComponentInChildren<PlateController>();
        plate3 = Slots[2].GetComponentInChildren<PlateController>();
        // Thực hiện logic xáo trộn ở đây
        if(plate1 != null){
            //plate1.GetComponent<Animator>().Play("disappear 1");
            Destroy(plate1.gameObject);
            
        }
         if(plate2 != null){
            //plate2.GetComponent<Animator>().Play("disappear 1");
            Destroy(plate2.gameObject);
        }
         if(plate3 != null){
            //plate3.GetComponent<Animator>().Play("disappear 1");
            Destroy(plate3.gameObject);
        }

        SpawnCake.GetComponent<SpawnCakes>().cakeCount = 0;
        shuffleCount -=1;
        if(shuffleCount == 0){
            text.text = "100";
            icon.SetActive(true);
        }
        else{
            text.text= shuffleCount+"";
        }
    }
}
