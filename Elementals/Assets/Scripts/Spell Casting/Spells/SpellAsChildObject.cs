using System.Collections;
using System.Collections.Generic;
using Spell_Casting.Spells;
using UnityEngine;

[AddComponentMenu("Spells/Spell As Child Object")]
public class SpellAsChildObject : InstantiateObjectSpell, ISpell
{
    public GameObject spellPrefab;
    
    [Range(0,10)]
    public int manaCost = 5;

    
    public float lifetime = 10;
    
    [Tooltip("Additional time after spell lifetime completes before the spell can be cast again")]
    public float additionalCastDelay = 1;

    private bool _canCast = true;
    
    public bool CastSpell(GameObject caster, Vector2 position, Vector2 direction)
    {
        if (!_canCast) 
            return false;
        return base.CastSpell(caster, position, direction);
        Debug.Log($"Cast by {caster}");
        var spell =Instantiate(spellPrefab, caster.transform);
        spell.transform.SetParent(caster.transform,false);
        _canCast = false;
        StartCoroutine(CastChildObject(spell, caster.transform));
        return true;
    }

    protected override GameObject InstantiateSpellObject(GameObject caster, Vector2 position, Vector2 direction)
    {
        if (!_canCast) return null;
        var spell =Instantiate(spellPrefab, caster.transform);
        spell.transform.SetParent(caster.transform,false);
        _canCast = false;
        StartCoroutine(CastChildObject(spell, caster.transform));
        return spell;
    }

    IEnumerator CastChildObject(GameObject spell, Transform casterTransform)
    {
        _canCast = false;
        float endTime = Time.time + lifetime;
        while (endTime > Time.time)
        {
            spell.transform.position = casterTransform.position;
            yield return null;
        }
        
        if (spell != null)
        {
            GameObject.Destroy(spell);
        }
        yield return new WaitForSeconds(additionalCastDelay);
        _canCast = true;
    }

    public float ManaCost => manaCost;
}
