using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    private Vector3 initialPosition; // Vị trí ban đầu của Plate
    private bool isDragging = false; // Trạng thái kéo thả chuột
    private Vector3 offset; // Offset giữa vị trí của chuột và vị trí của plate
    public bool isTouchingCircle = false;
    public bool isPlaced = false; // Nếu plate được đặt vào vị trí
    public Dashcircle parentCircle;
    public CakeController currentCake;
    public HashSet<Dashcircle> touchingCircles = new HashSet<Dashcircle>();
    private Camera secondCamera;
    private Camera mainCamera;
    private SpawnCakes spc;
    private CircleMatrixController csc;
    private GameOverController gameover;
   
    // Thêm thuộc tính cho AudioSource và AudioClip
    private GameObject audioSrc;
    private AudioSource audioSource;
    public AudioClip soundEffect;
    public AudioClip soundDisAppearEffect;
    private GameObject tong;
    
    private Animator animator;

    void Start()
    {
        initialPosition = transform.position; // Lưu vị trí ban đầu của Plate
        csc = FindObjectOfType<CircleMatrixController>();
        gameover = FindObjectOfType<GameOverController>();
        spc = FindObjectOfType<SpawnCakes>();
        tong = GameObject.Find("ButtonSwitch");

        secondCamera = GameObject.Find("Camera").GetComponent<Camera>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        audioSrc = GameObject.Find("Audio");
        if(audioSrc  !=null ){
            audioSource = audioSrc.GetComponent<AudioSource>();
        }
        
        animator = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        // Lưu offset khi kéo Plate
        if(!isPlaced){
           /*  if(transform.position != initialPosition){
                return;
            } */
            isDragging = true;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = secondCamera.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = secondCamera.ScreenToWorldPoint(mousePosition);
            offset = transform.position - worldPosition;
            Vector3 currentPosition = transform.position;
            currentPosition.y += 1.0f;
            transform.position = currentPosition;
            if (audioSource != null && soundEffect != null && PlayerPrefs.GetInt("isSound", 1) == 1)
            {
                audioSource.PlayOneShot(soundEffect);
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Dashcircle p = other.gameObject.GetComponent<Dashcircle>();
        if (p != null)
        {
            touchingCircles.Add(p);
            UpdateTouchingCircleStatus();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Dashcircle p = other.gameObject.GetComponent<Dashcircle>();
        if (p != null)
        {
            touchingCircles.Remove(p);
            UpdateTouchingCircleStatus();
        }
    }

    void UpdateTouchingCircleStatus()
    {
        if (touchingCircles.Count > 0)
        {
            isTouchingCircle = true;
            parentCircle = GetClosestCircle();
        }
        else
        {
            isTouchingCircle = false;
            parentCircle = null;
        }
    }

    Dashcircle GetClosestCircle()
    {
        Dashcircle closestCircle = null;
        float minDistance = float.MaxValue;

        foreach (var circle in touchingCircles)
        {
            float distance = Vector3.Distance(transform.position, circle.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCircle = circle;
            }
        }

        return closestCircle;
    }

    void OnMouseUp()
    {
        isDragging = false;
        if(isPlaced){
            return;
        }
        if (isTouchingCircle && parentCircle != null && !parentCircle.isFilled)
        {
            parentCircle.currentPlate = this;
            csc.placedCake  = currentCake;
            currentCake.flag = false;
            parentCircle.isFilled = true;
            currentCake.MoveCakePieces(this);
            parentCircle.parentHole.SetActive(false);
            this.transform.SetParent(parentCircle.transform);
            //currentCake.CheckCombo();
            
            transform.position = parentCircle.transform.position;
            transform.localPosition = new Vector3(0,0,0);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.Euler(0,0,0);//Quaternion.identity;
            if( !tong.GetComponent<SwitchBtn>().isSwitch)
                spc.cakeCount -= 1;
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            isPlaced = true;
           
        }
        else
        {
            if( !tong.GetComponent<SwitchBtn>().isSwitch)
                transform.position = initialPosition;
        }
    }


    void Disappear(){
        currentCake.Break();
    }
    void Playfx(){
        currentCake.PlayFx();
    }
    void PlaySoundDisappear(){
        if (audioSource != null && soundDisAppearEffect != null &&  PlayerPrefs.GetInt("isSound", 1) == 1)
        {
            audioSource.PlayOneShot(soundDisAppearEffect);
        }
    }

    void Update()
    {
        if (isDragging && !isPlaced)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = secondCamera.WorldToScreenPoint(transform.position).z;
            Vector3 newPosition = secondCamera.ScreenToWorldPoint(mousePosition) + offset;
            transform.position = newPosition;
        }
    }
    public void UnableDashedCircle(){
        parentCircle.dashedCircle.SetActive(false);
    }
}
