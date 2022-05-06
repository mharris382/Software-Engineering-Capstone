using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Texture2D targetCursor;
    
    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var mouseWP = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mouseWP;
        Cursor.visible = true;
        Cursor.SetCursor(targetCursor, new Vector2(0.5f, 0.5f), CursorMode.Auto);
    }
}
