using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button backBtn;
    public GameObject decorAnim;
    void Start()
    {
        backBtn.onClick.AddListener(ClickEvent);
        
    }
    public void ClickEvent(){
        foreach (PlateController plate in decorAnim.GetComponent<DecorAnim>().plates)
        {
            plate.gameObject.SetActive(true);
            plate.parentCircle.parentHole.SetActive(true);
            plate.parentCircle.dashedCircle.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

