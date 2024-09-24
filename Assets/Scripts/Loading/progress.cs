using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProgressBarLoader : MonoBehaviour
{
    public float fillTime = 2f; // Thời gian fill progress bar (giả sử là 2 giây)
    public float rotationSpeed = -240f; // Tốc độ quay của quả bóng, 360 độ mỗi giây

    public Image progressImage;
    public Image ballImage;
    public ParticleSystem fx;
    private float progressBarWidth;
    private Vector3 ballStartPosition;
    private string sceneName;

    void Start()
    {
        progressBarWidth = progressImage.rectTransform.rect.width;
        ballStartPosition = ballImage.rectTransform.localPosition;
        Debug.Log("ball: " + ballStartPosition);

        sceneName = "Home"; // Đặt tên scene cần load
        StartLoading();
    }

    public void StartLoading()
    {
        StartCoroutine(FillProgressBar());
    }

    IEnumerator FillProgressBar()
    {
        float timer = 0f;

        while (timer < fillTime)
        {
            timer += Time.deltaTime;

            // Tính toán độ dài hiện tại của progress bar
            float currentWidth = Mathf.Lerp(0f, progressBarWidth, timer / fillTime);
            progressImage.rectTransform.sizeDelta = new Vector2(currentWidth, progressImage.rectTransform.rect.height);

            // Di chuyển quả bóng
            ballImage.rectTransform.localPosition = new Vector3(ballStartPosition.x + currentWidth - 40f, ballStartPosition.y, ballStartPosition.z);

            // Quay quả bóng
            ballImage.rectTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

            yield return null; // Chờ một frame
        }

        // Đã fill đầy progress bar
        progressImage.rectTransform.sizeDelta = new Vector2(progressBarWidth, progressImage.rectTransform.rect.height);
        ballImage.rectTransform.localPosition = new Vector3(ballStartPosition.x + progressBarWidth, ballStartPosition.y, ballStartPosition.z);

        if (fx != null)
        {
            fx.Play();
        }
        Debug.Log("Loading complete!");

        // Chờ khoảng 3 giây trước khi load scene
        yield return new WaitForSeconds(2f);

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set.");
        }
    }
}
