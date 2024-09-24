using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinAmount = 0;
    public TextMeshProUGUI text;
    void Start()
    {
        text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = coinAmount+"";
        
    }

    // Update is called once per frame

    public void UpdateCoins(int amount){
        coinAmount += amount;
        text.text = coinAmount+"";
    }
}
