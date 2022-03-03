using System;
using System.Collections;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    
}



public class SpellCaster : MonoBehaviour
{
    [Serializable]
    class SpellConfig
    {
        public float manaCost = 0.5f;
        
        public GameObject spellPrefab;
        
        [Range(0,60)]
        public float autoDestroyTime = 5;

        [SerializeField] private float minSpawnSpeed = 10;
        
        public void Cast(SpellCaster caster)
        {
            if (caster._mana.HasMana(manaCost) == false)
            {
                //TODO: failure message
                return;
            }
            
            caster._mana.RemoveMana(manaCost);
            
            if (spellPrefab != null)
            {
                var instance = Instantiate(spellPrefab, caster.spellSpawnPoint.position, caster.spellSpawnPoint.rotation);
                
                //interface for extending the behavior of the spell
                ISpellDecorator[] spells = instance.GetComponents<ISpellDecorator>();
                if (spells != null && spells.Length > 0)
                    foreach (var spell in spells)
                        spell.OnCasted(caster);
                
                //initialize spawn speed
                if( instance.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    caster.gameObject.TryGetComponent<Rigidbody2D>(out var casterRb);
                    rb.velocity = casterRb != null ? casterRb.velocity : Vector2.zero;
                    
                    if (rb.velocity.sqrMagnitude < (minSpawnSpeed * minSpawnSpeed))
                    {
                        rb.velocity = caster.spellSpawnPoint.right * minSpawnSpeed;
                    }
                }
                
                //if auto destroy time is set being the coroutine that destroys it after the given time
                if (autoDestroyTime > 0) 
                    caster.StartCoroutine(DestroyAfterSeconds(instance, autoDestroyTime));
            }
        }
        
    }
    
    
    private CasterState _state;
    private IManaSource _mana;
    
    internal IManaSource ManaState
    {
        get
        {
            if (_mana == null)
            {
                _mana = GetComponentInChildren<IManaSource>();
            }
            return _mana;
        }
    }

    internal CasterState CasterState
    {
        get
        {
            if (_state == null)
            {
                _state = GetComponent<CasterState>();
            }
            return _state;
        }
    }
    
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private SpellConfig basicSpell;
    [SerializeField] private SpellConfig strongSpell;

    
    private bool HasMana => _mana.CurrentValue > 0.1f;
    
    private void Awake()
    {
        _state = GetComponent<CasterState>();
        _mana = GetComponentInChildren<ManaState>();
        
        if (spellSpawnPoint == null)
            spellSpawnPoint = transform;
    }

    private void Start()
    {
        if (_state == null)
        {
            Debug.LogError("Caster State missing on Spell Caster", this);
            return;
        }
        _state.BasicSpell.onCastTriggered.AddListener(BasicCast);
        _state.StrongSpell.onCastTriggered.AddListener(StrongCast);
    }

    private void BasicCast()
    {
        if(HasMana)
            basicSpell.Cast(this);
    }

    private void StrongCast()
    {
        if(HasMana)
            strongSpell.Cast(this);
    }


    private static IEnumerator DestroyAfterSeconds(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    
    
}


public interface ISpell
{
    void CastSpell(SpellCaster caster, Vector2 origin, Vector2 direction);
}

public interface ISpellDecorator
{
    void OnCasted(SpellCaster caster);
}