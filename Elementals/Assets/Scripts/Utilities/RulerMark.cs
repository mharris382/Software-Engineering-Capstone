using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class RulerMark : MonoBehaviour
{
    public int intervalDistance = 2;
    
    [Min(1)]
    public int smallInterval = 1;

    [Min(1)]
    public int largeInterval = 5;

    
    
    public bool showTextOnSmallIntervals = true;
    private TMPro.TextMeshProUGUI text;

    private TMPro.TextMeshProUGUI Text
    {
        get
        {
            if (text == null)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
                if (text == null) text = gameObject.AddComponent<TextMeshProUGUI>();
            }
            return text;
        }
    }
    private void Update()
    {
        var i = (transform.GetSiblingIndex() + 1) * intervalDistance;
        this.name = i.ToString();
        if (i % largeInterval == 0)
        {
            ShowText();
        }
        else if ((i % smallInterval) == 0)
        {
            if (showTextOnSmallIntervals)
                ShowText();
            else
                ClearText();
        }
        else
        {
            ClearText();
        }
        
    }

    private void ClearText()
    {
        Text.text = "";
    }

    private void ShowText()
    {
        Text.text = this.name;
    }
}
