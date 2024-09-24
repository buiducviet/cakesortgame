using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureMove : MonoBehaviour
{
    public GameObject startPoint;
    private float speed = 1100f;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private Vector3 initialPosition;

    void Start()
    {
        transform.localPosition = startPoint.transform.localPosition;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if(!isMoving && targetPosition != null)
        {
            StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        isMoving = true;
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = targetPosition;

        // Chờ một chút trước khi quay lại vị trí ban đầu
        yield return new WaitForSeconds(1f);

        targetPosition = initialPosition;
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = targetPosition;

        isMoving = false;
    }
}
