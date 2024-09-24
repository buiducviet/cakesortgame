using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorBtn : MonoBehaviour
{
   // public GameObject canvasDecor;
    public GameObject decorAnim;
    public Button decorBtn;
    public GameObject mainCanvas;
    public GameObject SubTable;
    // Start is called before the first frame update
    void Start()
    {
        decorBtn.onClick.AddListener(ClickEvent);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClickEvent(){
        decorAnim.SetActive(true);
        mainCanvas.SetActive(false);
        SubTable.SetActive(false);
        decorAnim.GetComponent<Animator>().Play("Show");
    }
}
