using System.Collections;
using UnityEngine;

public class coinController1 : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool isMoving = false;
    private SpriteRenderer spriteRenderer;
    private float fadeDuration = 1.0f; // Thời gian để fade-out hoàn toàn
    private Vector3 to;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 250 * Time.deltaTime);
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 8f * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                StartCoroutine(FadeOutAndDestroy());
                isMoving = false; // Ngừng di chuyển sau khi đến đích
            }
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Color color = spriteRenderer.color;
        float startAlpha = color.a;

        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            float blend = Mathf.Clamp01(t / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, 0.0f, blend);
            spriteRenderer.color = color;
            yield return null;
        }

        // Đảm bảo alpha được đặt về 0 sau khi hoàn thành fade-out
        color.a = 0.0f;
        spriteRenderer.color = color;

        // Hủy đối tượng
        Destroy(gameObject);
    }
}
