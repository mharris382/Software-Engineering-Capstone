using System;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [Serializable]
    class SpellConfig
    {
        [SerializeField] private float manaCost = 0.5f;
        [SerializeField] private GameObject spellPrefab;
    }
    private CasterState _state;
    private ManaState _mana;
    [SerializeField] private SpellConfig basicSpell;
    [SerializeField] private SpellConfig strongSpell;

    private void Awake()
    {
        _state = GetComponent<CasterState>();
        _mana = GetComponentInParent<ManaState>();
    }

    public void BasicCast()
    {
        
    }

    public void StrongCast()
    {
        throw new NotImplementedException();
    }
}