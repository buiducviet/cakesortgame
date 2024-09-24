using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    private List<PictureMove> listPictures;
    private GameObject fxChild;

    void Start()
    {
        listPictures = new List<PictureMove>();
        fxChild = transform.GetChild(0).gameObject;
        foreach (Transform child in fxChild.transform)
        {
           // Debug.Log("" + child.name);
            PictureMove picture = child.GetChild(0).GetComponent<PictureMove>();
            //Debug.Log("" + picture.gameObject.name);
            if (picture != null)
            {
                listPictures.Add(picture);
            }
        }
       // Debug.Log(listPictures.Count);

        Transfer();
    }

    void Update()
    {
        
    }

    public void Transfer()
    {
        //Debug.Log("1001");
        foreach (PictureMove picture in listPictures)
        {
            picture.targetPosition = Vector3.zero;
        }
    }

    public void UnTransfer()
    {
       // Debug.Log("1000");
        
        foreach (PictureMove picture in listPictures)
        {
            picture.targetPosition = picture.startPoint.transform.localPosition;
        }
      //  Debug.Log("1002");
    }
}
