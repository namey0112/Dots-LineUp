using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.Search;

public class LevelData : SerializedMonoBehaviour
{


    public LevelConfig LevelConfig;
    
    public int[,] lsID = new int[5, 5];
    public List<GameObject> dotPrefabs;
    public GameObject boxPrefab;

    private void Start()
    {
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
                    }
                }
                Instantiate(boxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
    

}



[System.Serializable]
public class LevelConfig
{
    
}
