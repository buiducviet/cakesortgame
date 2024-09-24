using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public CircleMatrixController matrix;
    public GameObject gameoverpannel;
 
    public void GameOver(){
        /* if(circleMatrixController.circleUnFilledCount == 0 ){
            Debug.Log("GameOver");
            Time.timeScale = 0;
        }  */
        int rows = matrix.circleMatrix.GetLength(0);
        int cols = matrix.circleMatrix.GetLength(1);

        matrix.circleUnFilledCount=0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!matrix.circleMatrix[i, j].isFilled)
                {
                    matrix.circleUnFilledCount++;
                }
            }
        }
        if( matrix.circleUnFilledCount == 0){
            gameoverpannel.SetActive(true);
        } 
    }

   
}
