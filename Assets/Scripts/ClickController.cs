using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public static ClickController instance;
    public DotBase currentDot;
    public LineUnit lineUnit;
    
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
                    var temp = Instantiate(lineUnit); 
                    temp.lineRenderer.positionCount = 2;
                    temp.lineRenderer.SetPosition(0, new Vector3(currentDot.transform.position.x, currentDot.transform.position.y, -3));
                    temp.lineRenderer.SetPosition(1, new Vector3(dotbase.transform.position.x, dotbase.transform.position.y, -3));
                    currentDot = dotbase;
                }
            }
        }
    }

    
}
