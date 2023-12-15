using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ElementAnimationSwitcher : MonoBehaviour
{
    public ElementContainer playerElement;
    [SerializeField] private RuntimeAnimatorController[] _controllers;

    [SerializeField] private RuntimeAnimatorController fireController;
    [SerializeField] private RuntimeAnimatorController waterController;
    [SerializeField] private RuntimeAnimatorController earthController;
    [SerializeField] private RuntimeAnimatorController thunderController;
    [SerializeField] private RuntimeAnimatorController airController;
    private Animator _anim;
    private RuntimeAnimatorController _defaultController;
    private Dictionary<Element, RuntimeAnimatorController> _elementControllers = new Dictionary<Element, RuntimeAnimatorController>();
    private List<RuntimeAnimatorController> racs;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _defaultController = _anim.runtimeAnimatorController;
        _elementControllers.Add(Element.Fire, fireController);
        _elementControllers.Add(Element.Water, waterController);
        _elementControllers.Add(Element.Earth, earthController);
        _elementControllers.Add(Element.Thunder, thunderController);
        _elementControllers.Add(Element.Air, airController);
        _anim.runtimeAnimatorController = _elementControllers[playerElement.Element];
    }

    private void Start()
    {
        playerElement.OnElementChanged += OnElementChanged;
        OnElementChanged(playerElement.Element);
    }

    private void OnDestroy()
    {
        playerElement.OnElementChanged -= OnElementChanged;
    }

    private void OnElementChanged(Element obj)
    {
        if (_elementControllers.ContainsKey(obj) == false || _elementControllers[obj] == null)
        {
            Debug.LogError($"Dictionary missing key: {obj}!", this);
            foreach (var runtimeAnimatorController in _controllers)
            {
                if (runtimeAnimatorController.name.Contains(obj.ToString()))
                {
                    _anim.runtimeAnimatorController = runtimeAnimatorController;
                    return;
                }
            }
            foreach (var acs in racs)
            {
                if (acs.name.Contains(obj.ToString()))
                {
                    _anim.runtimeAnimatorController = acs;
                    return;
                }
            }
        }
        var controller = _elementControllers[obj] != null ? _elementControllers[obj] : _defaultController;
        if (controller == null) return;
        if (_anim == null) {
            _anim = GetComponent<Animator>();
        }
        _anim.runtimeAnimatorController = controller;
    }
}
