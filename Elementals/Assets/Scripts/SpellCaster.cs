using System;
using System.Collections;
using UnityEngine;

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
            caster._mana.CurrentValue -= manaCost;
            if (spellPrefab != null)
            {
                var instance = Instantiate(spellPrefab, caster.spellSpawnPoint.position, caster.spellSpawnPoint.rotation);
                
                //interface for extending the behavior of the spell
                ISpell[] spells = instance.GetComponents<ISpell>();
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
                    caster.StartCoroutine(caster.DestroyAfterSeconds(instance, autoDestroyTime));
            }
        }
        
    }
    
    
    private CasterState _state;
    private ManaState _mana;

    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private SpellConfig basicSpell;
    [SerializeField] private SpellConfig strongSpell;

    
    private bool HasMana => _mana.CurrentValue > 0.1f;
    
    private void Awake()
    {
        _state = GetComponent<CasterState>();
        _mana = GetComponentInChildren<ManaState>();
        if (spellSpawnPoint == null) spellSpawnPoint = transform;
    }

    public void BasicCast()
    {
        basicSpell.Cast(this);
    }

    public void StrongCast()
    {
        strongSpell.Cast(this);
    }

    IEnumerator DestroyAfterSeconds(GameObject gameObject, float time)
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
    void OnCasted(SpellCaster caster);
}