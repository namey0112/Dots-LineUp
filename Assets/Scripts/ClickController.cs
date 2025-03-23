using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

public class ClickController : SerializedMonoBehaviour
{
    public static ClickController instance;
    public DotBase currentDot;
    public LineUnit lineUnit;
    public Pathfinding aStar;

    private void Awake()
    {
        instance = this;
    }

    public void HandleDotClicked(DotBase dotbase)
    {
        if (currentDot == null)
        {
            currentDot = dotbase;
        }
        else
        {
            if (currentDot != dotbase)
            {
                if (currentDot.id != dotbase.id)
                {
                    currentDot = dotbase;
                }
                else
                {

                    Vector2Int startPos = new Vector2Int((int)currentDot.colNum, (int)currentDot.rowNum);
                    Vector2Int endPos = new Vector2Int((int)dotbase.colNum, (int)dotbase.rowNum);

                    aStar.grid[(int)currentDot.colNum, (int)currentDot.rowNum].isBlocked = false;
                    aStar.grid[(int)dotbase.colNum, (int)dotbase.rowNum].isBlocked = false;

                    List<Vector2Int> path = aStar.FindPath(startPos, endPos);
                    Debug.Log(path.Count);
                    if (path.Count != 0)
                    {
                    var temp = Instantiate(lineUnit);
                    temp.lineRenderer.positionCount = path.Count;
                    
                        for (int i = 0; i < temp.lineRenderer.positionCount; i++)
                    {
                        var spawnPos = new Vector3(path[i].x - 2, path[i].y, -3);
                        Debug.Log(path[i]);
                        temp.lineRenderer.SetPosition(i, spawnPos);
                        aStar.grid[path[i].x, path[i].y].isBlocked = true;
                    }
                    currentDot = dotbase;
                }
            }
        }
    }
}
