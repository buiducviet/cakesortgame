using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCakes : MonoBehaviour
{
    public GameObject platePrefab; // Prefab của Plate
    public List<GameObject> cakePiecePrefabs; // Danh sách các prefab của CakePiece
    public GameObject[] spawnSlots; // Danh sách các vị trí spawn cố định
    public int cakeCount = 3;
    public int pieceKindSpawn = 5;
    public GameObject subTable;
    private Animator anim;
    public AudioClip sound;
    private GameObject audioSrc;
    private AudioSource audioSource;
    public CakeController cakeController;

    void Start()
    {
        if (spawnSlots.Length != 3)
        {
            Debug.LogError("Please provide exactly 3 spawn slots.");
            return;
        }
        anim = subTable.GetComponent<Animator>();
        audioSrc = GameObject.Find("Audio");

        // Lấy AudioSource từ cùng GameObject
        audioSource = audioSrc.GetComponent<AudioSource>();
    }
    /* void Awake(){
        SpawnPlatesWithCakes();
    } */

    void Update()
    {
        if (cakeCount == 0)
        {
            SpawnPlatesWithCakes();
            cakeCount = 3;
            anim.Play("Run");
        }
        /* if(cakeController!=null){
             if (!cakeController.isRotating)
                {
                    cakeController.RotateCakePiecesSlowly();
                }
        } */

    }

    void SpawnPlatesWithCakes()
    {
       // int from = (pieceKindSpawn >5)? (pieceKindSpawn-5): 0;
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            // Tạo Plate tại vị trí cố định
            GameObject spawnSlot = spawnSlots[i];
            GameObject plate = Instantiate(platePrefab, spawnSlot.transform.position, Quaternion.identity);

            // Đặt plate là con của vị trí spawn và localPosition là (0, 0, 0)
            plate.transform.SetParent(spawnSlot.transform);
            plate.transform.localPosition = Vector3.zero;
            plate.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            // Đặt tên của plate giống với tên của prefab
            plate.name = platePrefab.name;

            // Tạo CakeController
            cakeController = plate.GetComponentInChildren<CakeController>();
            if (cakeController != null)
            {
                int totalPieces = Random.Range(1, 6);
                int numberOfTypes = Random.Range(1, Mathf.Min(4, totalPieces + 1));
                HashSet<string> usedPieceNames = new HashSet<string>();
                List<int> piecesPerType = new List<int>();
                for (int j = 0; j < numberOfTypes; j++)
                {
                    int piecesLeft = totalPieces - piecesPerType.Sum();
                    if (j == numberOfTypes - 1)
                    {
                        piecesPerType.Add(piecesLeft);
                    }
                    else
                    {
                        int piecesThisType = Random.Range(1, piecesLeft - (numberOfTypes - j - 1) + 1);
                        piecesPerType.Add(piecesThisType);
                    }
                }

                for (int j = 0; j < numberOfTypes; j++)
                {
                    GameObject randomPiecePrefab;
                    string pieceName;
                    do
                    {
                        
                        randomPiecePrefab = cakePiecePrefabs[Random.Range(pieceKindSpawn-5, pieceKindSpawn)];
                        pieceName = randomPiecePrefab.name;
                    } while (usedPieceNames.Contains(pieceName));
                    usedPieceNames.Add(pieceName);

                    for (int k = 0; k < piecesPerType[j]; k++)
                    {
                        GameObject cakePiece = Instantiate(randomPiecePrefab, plate.transform.position, Quaternion.identity);
                        cakePiece.transform.SetParent(cakeController.transform);
                        cakePiece.transform.localPosition = Vector3.zero; 
                        cakeController.cakePieces.Add(cakePiece.GetComponent<CakePieceController>());
                        cakeController.cakePiecesName.Add(pieceName);
                        cakePiece.name = pieceName;
                        Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
                        cakePiece.transform.localRotation = targetRotation;
                        cakePiece.transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
                        cakePiece.tag = "CakePiece";

                        Vector3 eulerAngles = cakePiece.transform.localRotation.eulerAngles;
                        if (eulerAngles.x < 0) eulerAngles.x += 360;
                        if (eulerAngles.y < 0) eulerAngles.y += 360;
                        if (eulerAngles.z < 0) eulerAngles.z += 360;
                        cakePiece.transform.localRotation = Quaternion.Euler(eulerAngles);
                    }
                }

                // Cập nhật lại vị trí các mảnh bánh trên đĩa
               // cakeController.Start();
               cakeController.RotateCakePiecesSlowly();
            }
        }
    }
    
}
