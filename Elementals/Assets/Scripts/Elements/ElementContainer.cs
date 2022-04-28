using System;
using System.Collections;
using System.Collections.Generic;
using Elements;
using UnityEngine;

[CreateAssetMenu(menuName = "Containers/Element Container")]
public class ElementContainer : ScriptableObject
{

    public static ElementContainer Fire => ElementConfig.Instance.fireElement;
    public static ElementContainer Water => ElementConfig.Instance.waterElement;
    public static ElementContainer Thunder => ElementConfig.Instance.thunderElement;
    public static ElementContainer Earth => ElementConfig.Instance.earthElement;
    public static ElementContainer Air => ElementConfig.Instance.airElement;
    
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