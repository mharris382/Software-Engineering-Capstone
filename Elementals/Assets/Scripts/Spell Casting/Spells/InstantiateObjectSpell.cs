using System.Collections.Generic;
using Spell_Casting.Spells;
using UnityEngine;

public abstract class InstantiateObjectSpell : MonoBehaviour, ISpell
{
    [SerializeField]
    private float manaCost = 1;

    public float ManaCost => manaCost;

    public virtual bool CastSpell(GameObject caster, Vector2 position, Vector2 direction)
    {
        var go = InstantiateSpellObject(caster, position, direction);
        if (go == null) return false;
        var spellEventListeners = new List<SpellEventListener>();
        spellEventListeners.AddRange(gameObject.GetComponentsInChildren<SpellEventListener>() ?? new SpellEventListener[0]);
        spellEventListeners.AddRange( go.GetComponentsInChildren<SpellEventListener>() ?? new SpellEventListener[0]);
        
        var spellInfo = new SpellInfo()
        {
            caster = caster,
            SpellInstance = go,
            CastPosition = position,
            CastDirection = direction,
            CastTime = Time.time
        };
        foreach (var spellEventListener in spellEventListeners)
        {
            spellEventListener.SpellWasCast?.Invoke(spellInfo);
        }

        return true;
    }
    

    protected abstract GameObject InstantiateSpellObject(GameObject caster, Vector2 position, Vector2 direction);
}


public abstract class InstantiateObjectSpell<T> : InstantiateObjectSpell where T : Component
{
    protected override GameObject InstantiateSpellObject(GameObject caster, Vector2 position, Vector2 direction)
    {
        var obj = InstantiateSpell(caster, position, direction);
        return obj != null ? obj.gameObject : null;
    }

    protected abstract T InstantiateSpell(GameObject caster, Vector2 position, Vector2 direction);
}