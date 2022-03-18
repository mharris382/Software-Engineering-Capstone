using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var mouseWP = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mouseWP;
    }
}
