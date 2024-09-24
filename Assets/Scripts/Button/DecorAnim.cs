using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorAnim : MonoBehaviour
{
    public GameObject CanvasDecor;
    public PlateController[] plates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartPlay(){
        this.gameObject.SetActive(true);
    }
    public void EndPlay(){
        plates = FindObjectsOfType<PlateController>();
        foreach (PlateController plate in plates)
        {
            plate.gameObject.SetActive(false);
            plate.parentCircle.parentHole.SetActive(true);
            plate.parentCircle.dashedCircle.SetActive(false);
        }
        CanvasDecor.SetActive(true);
        this.gameObject.SetActive(false);
        
    }
    
}
