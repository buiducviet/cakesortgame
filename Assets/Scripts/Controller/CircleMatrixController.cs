using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMatrixController : MonoBehaviour
{
    public Dashcircle[,] circleMatrix; // Matrix chứa các GameObject Circle
    public int circleUnFilledCount ;
    public bool flagCakeMove = false;
    public bool flagCakePieceMove = false;
    public CakeController placedCake;
    public CakeController comboCake;
    public bool mainFlag =false;
    //public bool secondFlag =false;


    void Start()
    {
        // Gọi hàm để tạo ma trận Circle và kiểm tra các Circle lân cận
        PopulateCircleMatrix();
       
        //CheckNeighbors();
    }

    void PopulateCircleMatrix()
    {
        // Lấy số lượng hàng và cột từ ma trận đã có sẵn trong hierarchy
        GameObject[] rowObjects = GameObject.FindGameObjectsWithTag("row");
        int rows = rowObjects.Length;
        if (rows == 0)
        {
            
            return;
        }

        int columns = transform.GetChild(0).childCount;
        //circleUnFilledCount = rows*columns;

        // Khởi tạo ma trận Circle với kích thước phù hợp
        circleMatrix = new Dashcircle[rows, columns];
       
        // Điền ma trận Circle với các GameObject Circle từ hierarchy
        for (int i = 0; i < rows; i++)
        {
            Transform rowTransform = transform.GetChild(i);
            for (int j = 0; j < columns; j++)
            {
                GameObject circle = rowTransform.GetChild(j).gameObject;
                Dashcircle dashcircle = new Dashcircle();
                if(circle != null){
                    dashcircle = circle.GetComponent<Dashcircle>();
                   
                }
                circleMatrix[i, j] = dashcircle;
                circleMatrix[i, j].row_num = i;
                circleMatrix[i, j].col_num = j;
               // Debug.Log("đây: "+circleMatrix[i,j].name);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
