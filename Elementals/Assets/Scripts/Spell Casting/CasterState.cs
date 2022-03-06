﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(CasterController))]
public class CasterState : MonoBehaviour
{
    [FormerlySerializedAs("basicSpellEvents")] [SerializeField] CastEvents basicSpell;
    [FormerlySerializedAs("strongSpellEvents")] [SerializeField] CastEvents strongSpell;
    
    public bool Gathering { get; set; }
    public bool Casting { get; set; }
    public bool CastingStrong { get; set; }
    public bool AllowCast { get; set; }

    private CasterInput _input;
    public CasterInput input => _input ??= new CasterInput();
    public CastEvents BasicSpell => basicSpell;
    public CastEvents StrongSpell => strongSpell;

    public bool IsCasting => BasicSpell.IsCasting || StrongSpell.IsCasting;

    public void FinishCasts()
    {
        if(BasicSpell.IsCasting)BasicSpell.Finish();
        if(StrongSpell.IsCasting)StrongSpell.Finish();
    }

}
/// <summary>
/// this data container represents the intention of the caster
/// <para>the source of the intention is not important, it could
/// come from the player via input devices or from an AI or anywhere else</para>
/// <para>this data mirrors the actual caster state because we should only be able
/// to want to do things that we can do. i.e. the desired state should be able to
/// become the caster's actual state. </para> 
/// </summary>
public class CasterInput
{
    public bool Gathering { get; set; }
    public bool CastBasic { get; set; }
    public bool CastStrong { get; set; }
}

/// <summary>
/// This stores events related to spell casts. I'm not sure if this is technically a data oriented class,
/// but since the process of spell casting has so much back-and-forth between the animator and other systems
/// using an event container as part of our caster data container decouples the sender from the receiver of spell cast events   
/// </summary>
[Serializable]
public class CastEvents
{
     [Tooltip("This is called when the process of casting a spell is started")]
    public UnityEvent onCastStarted;
    
     [Tooltip("This is called when the spell is actually created")]
    public UnityEvent onCastTriggered;
    
     [Tooltip("This is called when the character is completely finished the casting process")]
    public UnityEvent onCastCompleted;

    public bool IsCasting { get; private set; }
    
    public void Start()
    {
        IsCasting = true;
        onCastStarted?.Invoke();
    }

    public void Finish()
    {
        IsCasting = false;
        onCastCompleted?.Invoke();
    }

    public void Trigger()
    {
        onCastTriggered?.Invoke();
    }
}


