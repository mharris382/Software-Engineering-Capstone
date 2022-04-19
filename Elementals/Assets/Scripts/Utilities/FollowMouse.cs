using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float lockZ = 10;
    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var mouseWP = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWP.z = lockZ;
        
        transform.position = mouseWP;
    }
}
