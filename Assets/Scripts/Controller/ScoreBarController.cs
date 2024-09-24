using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreBarController : MonoBehaviour 
{
    private int maxScore = 300;
    public int score = 0;
    public Image progressImage;
    public GameObject textSite;
    private float progressBarWidth;
    private float targetFillAmount;
    public float fillSpeed = 0.5f; // Tốc độ chuyển đổi thanh tiến độ (thay đổi theo nhu cầu)
    private TextMeshProUGUI text;

    void Start()
    {
        progressBarWidth = progressImage.rectTransform.rect.width;
        text = textSite.GetComponent<TextMeshProUGUI>();
        UpdateProgressBar();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        text.text = score + "/"+ maxScore;
        if (score > maxScore) score = maxScore; // Đảm bảo score không vượt quá maxScore
        StartCoroutine(SmoothFillProgressBar());
    }

    private void UpdateProgressBar()
    {
        if (maxScore > 0)
        {
            targetFillAmount = (float)score / maxScore;
        }
        else
        {
            targetFillAmount = 0; // Trong trường hợp maxScore là 0
        }
    }

    private IEnumerator SmoothFillProgressBar()
    {
        float startFillAmount = progressImage.fillAmount;
        float elapsedTime = 0f;

        // Đảm bảo rằng giá trị của targetFillAmount được cập nhật trước khi bắt đầu Coroutine
        UpdateProgressBar();

        while (elapsedTime < fillSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fillSpeed);
            progressImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        // Đảm bảo giá trị cuối cùng chính xác
        progressImage.fillAmount = targetFillAmount;
    }
}
