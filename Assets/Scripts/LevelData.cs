using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.Search;
using System.Data;

public class LevelData : SerializedMonoBehaviour
{

    //public static LevelData instance;
    public LevelConfig LevelConfig;
    
    public int[,] lsID = new int[5, 5];
    public Node[,] lsObstacle = new Node[5, 5];
    public List<GameObject> dotPrefabs;
    public GameObject boxPrefab;
    public Pathfinding aStar;


    private void Start()
    {
        //instance = this;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                lsObstacle[x, y] = new Node(x, y, false); // Mặc định tất cả các ô không bị chặn
            }
        }
        HandleSpawnDots();
    }

    private void HandleSpawnDots()
    {
        int tempID;
        Vector3 spawnPos;
        for (int i = 0; i < lsID.GetLength(0); i++)
        {
            for (int j = 0; j < lsID.GetLength(1); j++)
            {
                tempID = lsID[i, j];
                spawnPos = new Vector3(i-2f, j, 0);
                foreach (var item in dotPrefabs)
                {
                    if (item.GetComponent<DotNorm>().id == tempID)
                    {
                        item.GetComponent<DotNorm>().rowNum = j;
                        item.GetComponent<DotNorm>().colNum = i;
                        Instantiate(item, spawnPos, Quaternion.identity);
                        lsObstacle[i, j] = new Node(i, j, true);
                    }
                }
                Instantiate(boxPrefab, spawnPos, Quaternion.identity);
            }
        }
        aStar.grid = lsObstacle;
       
    }
    [Button]
    public void ShowGrid()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Debug.Log($"({lsObstacle[i, j].x}, {lsObstacle[i, j].y}) = {lsObstacle[i, j].isBlocked}");
            }
        }
    }
}



[System.Serializable]
public class LevelConfig
{
    
}
