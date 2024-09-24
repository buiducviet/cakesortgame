using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CakePieceController : MonoBehaviour {
    private string name;
    public bool isMoving = true;
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    public CakeController parentCake;
    public CircleMatrixController cmatrix;

    void Start () {
        name = this.gameObject.name;
        parentCake = transform.parent.GetComponent<CakeController>();
        cmatrix =  FindObjectOfType<CircleMatrixController>();
    }

    public void StartMove() {
        StartCoroutine(MoveCoroutine());
        //StartCoroutine(SetFlagAfterDelay(0.2f));
    }
    private IEnumerator SetFlagAfterDelay(float delay) {
        yield return new WaitForSeconds(delay); // Chờ trong khoảng thời gian delay
            // Sau đó đặt flag thành false
    }
    private IEnumerator MoveCoroutine() {
        cmatrix.flagCakePieceMove = true;
      //  parentCake.moving = true;
       // isMoving = true;
        if (SystemInfo.supportsVibration && (PlayerPrefs.GetInt("isVibration", 1)==1))
        {
            #if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
                StartCoroutine(StopVibrationAfterDelay(0.05f));
            #endif
        }
            
        while (transform.localPosition != targetPosition || transform.localRotation != targetRotation) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime * 9f);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 550f);
            yield return null;
        }
        cmatrix.flagCakePieceMove = false; 
        
        parentCake.flag = false;
       // isMoving = false;
       // parentCake.moving = false;
       
    }
    private IEnumerator StopVibrationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Không có API trực tiếp để dừng rung trong Unity, nhưng cách này tạo cảm giác rung nhẹ hơn
    }
}
