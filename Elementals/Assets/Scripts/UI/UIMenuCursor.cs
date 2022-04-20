using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuCursor : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (Cursor.lockState != CursorLockMode.Confined)
        {
            yield return null;
        }

        while (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
            yield return null;
        }
    }

    private void Update()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }
}
