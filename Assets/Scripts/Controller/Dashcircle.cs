using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashcircle : MonoBehaviour
{
    public GameObject dashedCircle; // Reference to the dashed circle
    public bool isFilled = false;
    public float rotationSpeed = 60f; // Speed of rotation
    public bool isRotating = false; // Flag to control rotation
    public PlateController currentPlate;
    public int row_num;
    public int col_num;
    public GameObject parentHole;


    // Called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
       
        var p = other.gameObject.GetComponent<PlateController>();
        if (p != null)
        {
            dashedCircle.SetActive(true);
            currentPlate = p;
            isRotating = true;
        }
    }

    // Called when another collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        var p = other.gameObject.GetComponent<PlateController>();
        if (p!=null)
        {
            dashedCircle.SetActive(false);
            currentPlate = null;
            isRotating = false;
           
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            dashedCircle.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
