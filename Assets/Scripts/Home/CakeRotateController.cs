using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRotateController : MonoBehaviour
{
    public float rotationSpeed = 10f; // Tốc độ xoay

    void Update()
    {
        // Xoay đối tượng theo trục Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
