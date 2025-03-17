using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DotBase : MonoBehaviour
{
    public int id;
    public int rowNum;
    public int colNum;

    public abstract void Init();
    protected void OnMouseDown()
    {
        ClickController.instance.HandleDotClicked(this);
        
    }
}
