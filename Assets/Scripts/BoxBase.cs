using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBase : MonoBehaviour
{
    //public int id;
    public int rowNum;
    public int colNum;

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            DragController.instance.HandleDrawLineOnDrag(this);
        }
        else
        {
            if (DragController.instance.currentBox != null)
            {
                DragController.instance.currentBox = null;
            }
        }
    }
    
}
