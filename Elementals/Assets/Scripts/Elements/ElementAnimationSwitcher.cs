using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ElementAnimationSwitcher : MonoBehaviour
{
    public ElementContainer playerElement;
    [SerializeField] private RuntimeAnimatorController[] _controllers;

    private Animator _anim;
    private RuntimeAnimatorController _defaultController;
    private Dictionary<Element, RuntimeAnimatorController> _elementControllers = new Dictionary<Element, RuntimeAnimatorController>();
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _defaultController = _anim.runtimeAnimatorController;
        if (playerElement == null) playerElement = Resources.Load<ElementContainer>("PlayerElement");

        Array elements = Enum.GetValues(typeof(Element));
        foreach (var element in elements)
        {
            var e = (Element) element;
            var controller = _controllers.FirstOrDefault(t => t.name.Contains(element.ToString()));
            if(controller == null)Debug.LogError($"Couldn't find controller for element {element}",this);
            _elementControllers.Add(e, controller);
        }
        foreach (var kvp in _elementControllers)
        {
            if (kvp.Value == null)
            {
                Debug.LogError($"No Animator Controller found for Element {kvp.Key}!", this);
            }
        }
    }

    private void Start()
    {
        playerElement.OnElementChanged += OnElementChanged;
        OnElementChanged(playerElement.Element);
    }

    private void OnElementChanged(Element obj)
    {
        if (_elementControllers.ContainsKey(obj) == false) 
            Debug.LogError($"Dictionary missing key: {obj}!", this);
        
        var controller = _elementControllers[obj] != null ? _elementControllers[obj] : _defaultController;
        _anim.runtimeAnimatorController = controller;
    }
}
