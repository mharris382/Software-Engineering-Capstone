using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/Element Container")]
public class ElementContainer : ScriptableObject
{
    [SerializeField]
    private Element element;

    public event Action<Element> OnElementChanged;

    public Element Element
    {
        get => element;
        set
        {
            if (element != value)
            {
                element = value;
                OnElementChanged?.Invoke(element);
            }
        }
    }
}