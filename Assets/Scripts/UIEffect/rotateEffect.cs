using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    // Tốc độ quay (độ/giây)
    private float rotationSpeed = 50f;

    // Update được gọi mỗi khung hình (frame)
    void Update()
    {
        // Quay quanh trục Z của đối tượng (local axis)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
