using System;
using System.Collections;
using DefaultNamespace;
using Spell_Casting.Spells;
using UnityEngine;


//TODO: need to update documentation.  Spell caster no longer requires CasterState.
//TODO: If mana source is not added, Unlimited Mana Source will be created as the mana source
/// <summary>
/// No longer requires a caster state
/// Mana Source is still required in the code, but doesn't need to be added manaually anymore. If it is missing an Unlimited Mana Source will be created. 
/// The only component that must be added manually to the hierarchy is the ISpellProvider <see cref="ISpellProvider"/> 
/// </summary>
public class SpellCaster : MonoBehaviour
{
    private IManaSource _mana;
    private ISpellProvider _spellProvider;
    
    internal IManaSource ManaState
    {
        get
        {
            if (_mana == null)
            {
                _mana = GetComponentInChildren<IManaSource>();
                if (_mana == null) {
                    _mana = gameObject.AddComponent<UnlimitedMana>();
                    Debug.Log("No mana source found on caster.  Adding UnlimitedMana source.",this);
                }
            }
            return _mana;
        }
    }
   
    
    [SerializeField] private Transform spellSpawnPoint;
    public GameObject[] castFX;


    private bool HasMana => ManaState == null || ManaState.HasMana(0.01f);
    
    private void Awake()
    {
        _spellProvider = GetComponentInChildren<ISpellProvider>();
        _mana = ManaState;
        
        if (spellSpawnPoint == null) spellSpawnPoint = transform;
    }

    private void Start()
    {
        Debug.Assert(_spellProvider != null, "No Spell Provider found in children of Spell caster!", this);
        StartListeningToCasterEvents();
        
        
        void StartListeningToCasterEvents()
        {
            var state = GetComponent<CasterState>();
            if (state != null)
            {
                state.BasicSpell.onCastTriggered.AddListener(BasicCast);
                state.StrongSpell.onCastTriggered.AddListener(StrongCast);
            }
        }
    }
    
    

    
    
    
    public void BasicCast() => TryCastSpell(SpellNames.FastAttackSpell);
    public void StrongCast() => TryCastSpell(SpellNames.StrongAttackSpell);

    void TryCastSpell(string spellName)
    {
        ISpell spell;
         if (TryGetSpell(out spell) && CastSpell(spell))
            SpawnCastFX();


         void SpawnCastFX()
         {
             if (castFX == null || castFX.Length == 0) return;
             foreach (var fx in castFX)
             {
                 if (fx == null) continue;
                 var t = transform;
                 Instantiate(fx, t.position, t.rotation);
             }
         }
         bool TryGetSpell(out ISpell spell1)
         {
             spell1 = _spellProvider.GetSpell(spellName);
             if (spell1 == null)
             {
                 Debug.LogError($"Spell Provider is Missing spell with name {spellName}", this);
                 return false;
             }

             return true;
         }
         
    }
    


    private bool CastSpell(ISpell spell)
    {
        bool TryCastSpell()
        {
            var position = spellSpawnPoint.position;
            var direction = spellSpawnPoint.right;
            if (spell.CastSpell(gameObject, position, direction))
            {
                Debug.DrawRay(position,direction, Color.blue, 1);
                return true;
            }
            return false;
        }
        var mana = ManaState;
        if (mana.HasMana(spell.ManaCost))
        {
            mana.RemoveMana(spell.ManaCost);
            return TryCastSpell();
        }
        return false;
    }


    #region OBSOLETE CODE

    [Obsolete("Replaced with ISpell")]
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
    
    [HideInInspector]
    [Obsolete("Replaced by SpellProvider.Get(SpellNames.StrongSpell")]
    [SerializeField] private SpellConfig basicSpell;
    
    [HideInInspector]
    [Obsolete("Replaced by SpellProvider.Get(SpellNames.StrongSpell")]
    [SerializeField] private SpellConfig strongSpell;
    
    [Obsolete("Spell Caster Should not be handling object destruction")]
    private static IEnumerator DestroyAfterSeconds(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    
}


public interface ISpellDecorator
{
    void OnCasted(SpellCaster caster);
}