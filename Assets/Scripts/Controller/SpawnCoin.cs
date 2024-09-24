using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnCoin : MonoBehaviour
{

    public GameObject coinPrefab;
    public GameObject Destination;
    public Vector3 from;
    public Vector3 to;
   // public int coinAmount;
    // Start is called before the first frame update
    void Start()
    {
        to = Destination.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnCoins(int coinAmount){
        if(coinPrefab != null){
            for (int i = 0; i < coinAmount; i++){
                GameObject coin = Instantiate(coinPrefab, from, Quaternion.identity);
                coin.GetComponent<coinController1>().targetPosition = new Vector3(to.x, to.y, to.z);
                coin.GetComponent<coinController1>().isMoving = true;
            }
        }
    }
}
