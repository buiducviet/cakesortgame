using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{  
    public float minAngle = 0f;     // Góc tối thiểu
    public float maxAngle = 10f;    // Góc tối đa
    public float swingSpeed = 2f;   // Tốc độ đung đưa

    void Update()
    {
        // Tính toán góc quay hiện tại sử dụng Mathf.PingPong
        float angle = Mathf.PingPong(Time.time * swingSpeed, maxAngle - minAngle) + minAngle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
