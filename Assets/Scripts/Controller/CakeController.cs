
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;


public class CakeController : MonoBehaviour
{
    public List<CakePieceController> cakePieces;
    public HashSet<string> cakePiecesName = new HashSet<string>();
    public float rotationSpeed = 10f;
    private int currentPieceIndex = 1;
    public PlateController plate;
    public bool isRotating = true;
    private CircleMatrixController cmatrix;
    public  List<Dashcircle> adjacentCircles;
    private Dashcircle up, downn, right, left;
    private GameOverController gameover;
    private ScoreBarController score;
    private CoinController coin;
    private shakecake dish;
    private SpawnCoin spawnCoin;
    private bool hasCakeDisappeared = false;
    private Quaternion tgRotation;
    private Quaternion[] tgq= new Quaternion[6];
    public ParticleSystem fx;
    public bool moving = false;
    public bool disappear = false;
    public Animator animator;
    private Quaternion targetQuaternion;
    private Quaternion newQuaternion;
    private static bool isAnyCakeMoving = false;
    public bool checkCombo;
    public bool flag = false;
    private string nameComboPiece;
    private CakeController cake1, cake2;
    public bool secondFlag = false;

   


    public  void Start()
    {
        tgq[0] = new Quaternion(0, 1f, 0, 0);
        tgq[1] = new Quaternion(0.5f, 0.8660254f, 0, 0);
        tgq[2] = new Quaternion(0.8660254f, 0.5f, 0, 0);
        tgq[3] = new Quaternion(1f, 0, 0, 0);
        tgq[4] = new Quaternion(0.8660254f, -0.5f, 0, 0);
        tgq[5] = new Quaternion(0.5f, -0.8660254f, 0, 0); 
        
        score = FindObjectOfType<ScoreBarController>();
        gameover = FindObjectOfType<GameOverController>();
        cakePieces = new List<CakePieceController>();
        plate = transform.parent.GetComponent<PlateController>();
        plate.currentCake = this;
 
        animator = this.plate.gameObject.GetComponent<Animator>();
        dish = FindObjectOfType<shakecake>();

        foreach (Transform child in transform)
        {
            CakePieceController cakePieceController = child.GetComponent<CakePieceController>();
            cakePieces.Add(cakePieceController);
            cakePiecesName.Add(cakePieceController.name);
        }

        cmatrix = FindObjectOfType<CircleMatrixController>();
        coin = FindObjectOfType<CoinController>();
        spawnCoin = FindObjectOfType<SpawnCoin>();

        if (cmatrix == null)
        {
            Debug.LogError("CircleMatrixController not found!");
        }

    }

    public void CakeDisappear()
    {

        if ((cakePieces.Count == 0 || (cakePieces.Count == 6 && AllPiecesSameType())))
        {
            moving = true;
            this.plate.parentCircle.dashedCircle.SetActive(false);
            
            this.plate.parentCircle.parentHole.SetActive(true);
           
           if (!hasCakeDisappeared) // Kiểm tra cờ để xác nhận chỉ thực hiện một lần
            {
                
                animator.Play("disappear 1"); 
                this.plate.parentCircle.dashedCircle.SetActive(false);
                this.plate.parentCircle.isFilled = false;
                hasCakeDisappeared = true;
                if(this.cakePieces.Count >0){
                    score.IncreaseScore(int.Parse(this.cakePieces[0].gameObject.name+5));
                    coin.UpdateCoins(5);
                    dish.GetComponent<Animator>().Play("shakecake");
                    spawnCoin.from= plate.gameObject.transform.position;
                    spawnCoin.SpawnCoins(3);
                }
               
               

            }

            this.plate.parentCircle.dashedCircle.SetActive(false);
           
          
        }
    }
    public void PlayFx(){
         fx.Play();
    }

    bool AllPiecesSameType()
    {
        if (cakePieces.Count == 0) return false;
        string type = cakePieces[0].name;
        foreach (var piece in cakePieces)
        {
            if (piece.name != type) return false;
        }
        return true;
    }
    public void Break(){
        StartCoroutine(FadeOutAndDisable());
    }

   IEnumerator FadeOutAndDisable()
   {
        float duration = 0.05f;
        float elapsed = 0f;

        List<Renderer> renderers = new List<Renderer>();
        foreach (var piece in cakePieces)
        {
            Renderer renderer = piece.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderers.Add(renderer);
            }
        }
        while (elapsed < duration)
        {
            
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            foreach (Renderer renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }
            yield return null;
        }
        
        this.plate.parentCircle.dashedCircle.SetActive(false);
        Destroy(this.plate.gameObject);
    }  

    public void RotateCakePiecesSlowly()
    {
        StartCoroutine(RotateCakePiecesCoroutine());
    }

    private IEnumerator RotateCakePiecesCoroutine()
    {
        while (!check())
        {
           Quaternion baseQuaternion = cakePieces[0].transform.localRotation;
           
            for (int i = 1; i < cakePieces.Count; i++)
            {
                GameObject piece = cakePieces[i].gameObject;
                if( baseQuaternion == tgq[5] ){
                    targetQuaternion = tgq[0];
                }
                else {
                    for(int j = 0; j <5; j++){
                        if(tgq[j]== baseQuaternion){
                            int temp = (i+j)%6;
                            targetQuaternion = tgq[temp];
                        }
                    }
                }
                Quaternion currentQuaternion =  piece.transform.localRotation;
                if (currentQuaternion != targetQuaternion)
                {
                    
                    piece.transform.localRotation = Quaternion.RotateTowards(piece.transform.localRotation, targetQuaternion, Time.deltaTime * rotationSpeed);
                }

            }
            yield return null;
        }
    }

    public bool check()
    {
        if (cakePieces.Count == 0)
        {
            return true;
        }
        Quaternion baseQuaternion = cakePieces[0].transform.localRotation;
         for (int i = 1; i < cakePieces.Count; i++)
        {
            GameObject piece = cakePieces[i].gameObject;
            if( baseQuaternion == tgq[5] ){
                targetQuaternion = tgq[0];
            }
            else {
                for(int j = 0; j <5; j++){
                    if(tgq[j]== baseQuaternion){
                        int temp = (i+j)%6;
                        targetQuaternion = tgq[temp];
                    }
                }
            }
            Quaternion currentQuaternion =  piece.transform.localRotation;
            if (currentQuaternion != targetQuaternion)
            {
                isRotating = false;
                return false;
            }
        }
        isRotating = false;
        return true; 
    }


    void Update()
    {
        if(cmatrix.placedCake!=null && !cmatrix.flagCakeMove && !cmatrix.placedCake.moving){
            cmatrix.placedCake.MoveCakePieces(cmatrix.placedCake.plate);
            cmatrix.placedCake = null;
        } 
        if (!cmatrix.flagCakeMove && !moving && this.plate.isPlaced )
        {
            MoveCakePieces(this.plate);
            
                
        }
    }
  
    public void MoveCakePieces(PlateController platee)
    {
        cmatrix.flagCakeMove = true;
        //Debug.Log(this.plate.parentCircle.gameObject.name);
        if (platee == null)
        {
            Debug.LogError("Plate is null.");
           return;
        }
        if (platee.isPlaced)
        {  
            if (platee.parentCircle == null)
            {
                Debug.LogError("ParentCircle is null in Plate.");
                return;
            }        
            Dashcircle circle = platee.parentCircle;

            int x = circle.row_num;
            int y = circle.col_num;

            if (x + 1 > cmatrix.circleMatrix.GetLength(0) - 1 || !cmatrix.circleMatrix[x + 1, y].isFilled) downn = null;
            else downn = cmatrix.circleMatrix[x + 1, y];

            if (x - 1 < 0 || !cmatrix.circleMatrix[x - 1, y].isFilled) up = null;
            else up = cmatrix.circleMatrix[x - 1, y];

            if (y + 1 > cmatrix.circleMatrix.GetLength(1) - 1 || !cmatrix.circleMatrix[x, y + 1].isFilled) right = null;
            else right = cmatrix.circleMatrix[x, y + 1];

            if (y - 1 < 0 || !cmatrix.circleMatrix[x, y - 1].isFilled) left = null;
            else left = cmatrix.circleMatrix[x, y - 1];

            checkCombo = false;
            foreach(string s in platee.currentCake.cakePiecesName){
                int matchingSets= 0;
                if (up != null && up.currentPlate != null  )
                    if(up.currentPlate.currentCake.cakePiecesName.Contains(s)) {
                        cake1 = up.currentPlate.currentCake;
                        matchingSets++;
                    }
                if (downn != null && downn.currentPlate != null  )
                    if (downn.currentPlate.currentCake.cakePiecesName.Contains(s)) 
                    {
                        if (cake1!= null){
                            if(cake2!= null){
                                if( downn.currentPlate.currentCake.cakePiecesName.Count == 1){
                                    cake2 = downn.currentPlate.currentCake;
                                }
                            }
                            else {
                                cake2 = downn.currentPlate.currentCake;
                            }
                            
                        }
                        else {
                            cake1 = downn.currentPlate.currentCake;
                        }
                        matchingSets++;
                    }
                    
                if (right != null && right.currentPlate != null  )
                    if (right.currentPlate.currentCake.cakePiecesName.Contains(s)) 
                    { 
                        if (cake1 != null){
                            if(cake2!= null){
                                if( right.currentPlate.currentCake.cakePiecesName.Count == 1){
                                    cake2 = right.currentPlate.currentCake;
                                }
                            }
                            else {
                                cake2 = right.currentPlate.currentCake;
                            }
                        }
                        else {
                            cake1 = right.currentPlate.currentCake;
                        }
                        matchingSets++;
                    }
                if (left != null && left.currentPlate != null )
                    if (left.currentPlate.currentCake.cakePiecesName.Contains(s)) 
                    { 
                        if (cake1!= null){
                            if(cake2!= null){
                                if( left.currentPlate.currentCake.cakePiecesName.Count == 1){
                                    cake2 = left.currentPlate.currentCake;
                                }
                            }
                            else {
                                cake2 = left.currentPlate.currentCake;
                            }
                        }
                        else {
                            cake1 = left.currentPlate.currentCake;
                        }
                        matchingSets++;
                    }
                if(matchingSets >=2) {
                    Debug.Log("true");
                    nameComboPiece = s;
                    checkCombo = true;
                }
            }
            if(checkCombo){
                Debug.Log("combo");
                TransferCombo3Cakes(cake1, platee.currentCake, cake2, platee.currentCake.nameComboPiece);
            }
            adjacentCircles = new List<Dashcircle> { up, downn, right, left};
            foreach (var adjacentCircle in adjacentCircles)
            {
                if (adjacentCircle != null && adjacentCircle.currentPlate != null && adjacentCircle.currentPlate.isPlaced  &&  !adjacentCircle.currentPlate.currentCake.moving)
                {
                    if(!checkCombo)
                        Transfer2CakePieces(platee.currentCake, adjacentCircle.currentPlate.currentCake);
                }
            }
            adjacentCircles.Clear();
        }
        cmatrix.flagCakeMove = false;
   }
    public void TransferPiece(CakeController sourceCake, CakeController targetCake, GameObject cakePiece){
        if(targetCake.cakePieces.Count == 0){
            return;
        }
        flag = true;
        float targetrotation;
        sourceCake.cakePieces.Remove(cakePiece.GetComponent<CakePieceController>());
        int index = targetCake.cakePieces.FindLastIndex(x => x.gameObject.name == cakePiece.gameObject.name); 
        int first_samepiece_index = targetCake.cakePieces.FindIndex(x => x.gameObject.name == cakePiece.gameObject.name);
        for(int m = 0; m <= 5;m++){
            if(first_samepiece_index >= targetCake.cakePieces.Count || first_samepiece_index < 0){
                Debug.Log ("this is wrong");
                return;
            }
            if (tgq[m]==targetCake.cakePieces[first_samepiece_index].gameObject.transform.localRotation ){
                int temp = (m+index+1-first_samepiece_index)%6;
                    tgRotation = tgq[temp];
            }  
        } 
        cakePiece.transform.SetParent(targetCake.transform);
        if(index < 0 ){
            return;
        }          
        
        int count = targetCake.cakePieces.Count;
        if (index  <= count-2){
            for(int k = index+1; k < count; k++){
                if (targetCake.cakePieces[k].gameObject.transform.localRotation == tgq[5]){
                    newQuaternion = tgq[0];

                }
                else{
                    for(int m = 0; m < 5;m++){
                        if (tgq[m]==targetCake.cakePieces[k].gameObject.transform.localRotation )
                            newQuaternion = tgq[m+1];
                    }
                } 

                targetCake.cakePieces[k].gameObject.transform.localRotation = newQuaternion;

            }
        }
        
        cakePiece.GetComponent<CakePieceController>().targetRotation  = tgRotation;
        cakePiece.GetComponent<CakePieceController>().StartMove();
        
        targetCake.cakePiecesName.Add(cakePiece.gameObject.name);
        if (!sourceCake.cakePieces.Any(piece => piece.gameObject.name == cakePiece.gameObject.name))
        {
            sourceCake.cakePiecesName.Remove(cakePiece.gameObject.name);
        }
        if(index+1 > targetCake.cakePieces.Count || index +1< 0){
            Debug.Log(index+1 +" and "+ targetCake.cakePieces.Count);
        }
        targetCake.cakePieces.Insert(index+1, cakePiece.GetComponent<CakePieceController>());
        
        cakePiece.transform.SetSiblingIndex(index+1);
        
    }
    public void TransferCombo3Cakes(CakeController cake1, CakeController cake2, CakeController cake3, string s){
        bool cake1HasSingleType = cake1.cakePiecesName.Count == 1;
        bool cake2HasSingleType = cake2.cakePiecesName.Count == 1;
        bool cake3HasSingleType = cake3.cakePiecesName.Count == 1;
        CakeController targetCake;
        CakeController sourceCake;
        CakeController tempCake;

        if (cake1HasSingleType) {
            if(cake3HasSingleType){
                if(cake1.cakePieces.Count >  cake3.cakePieces.Count){
                    targetCake = cake1;
                }
                else {
                    targetCake = cake3;
                }
            }
            else {
                targetCake = cake1;
            }
        }else{
            if(cake3HasSingleType){
                targetCake = cake3;
            }
            else{
                int countCake1 =cake1.cakePieces.Count(element => element.gameObject.name== s);
                int countCake3 = cake3.cakePieces.Count(element => element.gameObject.name== s);
                if(countCake1 > countCake3) {
                    targetCake = cake1;
                }
                else {
                    targetCake = cake3;
                }
            }
        }
        sourceCake = (targetCake == cake1) ? cake3 : cake1;
        tempCake = cake2;
        int i = targetCake.cakePieces.Count - 1;
        int j = sourceCake.cakePieces.Count - 1;
        while (i < 5 && j >= 0 && !cmatrix.flagCakePieceMove ){
            GameObject cakePiece = sourceCake.cakePieces[j].gameObject;
            if(cakePiece.gameObject.name == s){
                if (!flag && tempCake.cakePieces.Count < 6){
                    TransferPiece(sourceCake, tempCake, cakePiece);
                } 
                //while(flag);
                if(targetCake.cakePieces.Count <6 && !flag )
                    TransferPiece(tempCake, targetCake, cakePiece);
                i++;
                j--;
            }
            else
            {
                j--;
            }
           
        }
        targetCake.disappear = true;
        if (!sourceCake.isRotating && !sourceCake.check())
        {
            sourceCake.RotateCakePiecesSlowly(); 
        }
        sourceCake.CakeDisappear();
        targetCake.CakeDisappear();
        gameover.GameOver();
        cmatrix.flagCakeMove = false;
        return;
    }
    public void Transfer2CakePieces(CakeController cake1, CakeController cake2){
        if (cake2 == null)
        {
            return;
        }
        HashSet<string> commonElementsCake1_2 = new HashSet<string>(cake1.cakePiecesName);
        commonElementsCake1_2.IntersectWith(cake2.cakePiecesName);
        
        if (commonElementsCake1_2.Count == 0)
        {
            return;
        }
        
        bool cake1HasSingleType = cake1.cakePiecesName.Count == 1;
        bool cake2HasSingleType = cake2.cakePiecesName.Count == 1;
    
        CakeController targetCake = null;

        if (cake2HasSingleType)
        {
            if(cake1HasSingleType){
                if(cake1 == cmatrix.placedCake){
                    targetCake = cake1;
                }
                else {
                    targetCake = cake2;
                }
            }
            else    targetCake = cake2;    
        }
        else 
        { 
            if(cake1HasSingleType)
                targetCake = cake1;
            else{
                string s =cake1.cakePieces[cake1.cakePieces.Count - 1].gameObject.name;
                if(cake2.cakePiecesName.Contains(s))
                {   int countCake1 =cake1.cakePieces.Count(element => element.gameObject.name== s);
                    int countCake2 = cake2.cakePieces.Count(element => element.gameObject.name== s);
                    if(countCake1 > countCake2) {
                        targetCake = cake1;
                    }
                    else {
                        targetCake = cake2;
                    }
                }
                else {
                    s = cake2.cakePieces[cake2.cakePieces.Count - 1].gameObject.name;
                    if(cake1.cakePiecesName.Contains(s))
                    {   int countCake1 =cake1.cakePieces.Count(element => element.gameObject.name== s);
                        int countCake2 = cake2.cakePieces.Count(element => element.gameObject.name== s);
                        if(countCake1 > countCake2) {
                            targetCake = cake1;
                        }
                        else {
                            targetCake = cake2;
                        }
                    }
                    else {
                        targetCake = cake1;
                    }
                }
            }
        }
        CakeController sourceCake = (targetCake == cake1) ? cake2 : cake1;
        int i = targetCake.cakePieces.Count - 1;
        int j = sourceCake.cakePieces.Count - 1;
        while (i < 5 && j >= 0 && !cmatrix.flagCakePieceMove)
        {
            GameObject cakePiece = sourceCake.cakePieces[j].gameObject;
            
                if (targetCake.cakePiecesName.Contains(cakePiece.gameObject.name))
                {
                   
                    TransferPiece(sourceCake, targetCake, cakePiece);
                    i++;
                    j--;
                  
                }
                else
                {
                    j--;
                }
        }
        targetCake.disappear = true;
        if (!sourceCake.isRotating && !sourceCake.check())
        {
            sourceCake.RotateCakePiecesSlowly(); 
        }
        sourceCake.CakeDisappear();
        targetCake.CakeDisappear();
        gameover.GameOver();
        cmatrix.flagCakeMove = false;

        return;
    }

}
    



 