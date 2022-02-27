using System;
using UnityEngine;

public class CastAnimationEvents : MonoBehaviour
{
    private SpellCaster _caster;

    public void BasicCast()
    {
        _caster.BasicCast();
    }
    
    public void StrongCast()
    {
        _caster.StrongCast();
    }

    private void Awake()
    {
        _caster = GetComponentInParent<SpellCaster>();
        
    }
}