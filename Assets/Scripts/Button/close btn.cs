using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public GameObject camera;
    private Animator anim;

    void Start()
    {
        anim = camera.GetComponent<Animator>();

        // Đảm bảo rằng nút này có một component Button
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    void OnClick()
    {
        //Debug.Log("111");
         anim.Play("UseHammerReverse");
    }
}
