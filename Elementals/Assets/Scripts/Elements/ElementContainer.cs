using System;
using System.Collections;
using System.Collections.Generic;
using OneLine;
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


[System.Serializable]
public class OptionalElementContainer
{

    [SerializeField] private ElementContainer dynamicElement;
    [SerializeField] private Element constantElement;

    public Element Element
    {
        get => dynamicElement == null ? constantElement : dynamicElement.Element;
        set
        {
            if (dynamicElement != null) dynamicElement.Element = value;
            constantElement = value;
        }
    }
}