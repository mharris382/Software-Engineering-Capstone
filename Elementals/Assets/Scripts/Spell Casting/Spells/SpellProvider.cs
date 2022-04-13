using System.Collections.Generic;
using UnityEngine;

namespace Spell_Casting.Spells
{
    [AddComponentMenu("Spells/Providers/Spell Provider")]
    public class SpellProvider : MonoBehaviour, ISpellProvider
    {
        [System.Serializable]
        private class SpellKey
        {
            public string key;
            public UnityEngine.Object spell;
            public ISpell GetSpell()
            {
                if (spell is GameObject)
                {
                    return ((GameObject) spell).GetComponent<ISpell>();
                }
                return spell as ISpell;
            } 
        }

        [SerializeField] private SpellKey[] spells;
        Dictionary<string, ISpell> _spellLookup = new Dictionary<string,ISpell>();

        private void Awake()
        {
            foreach (var spellKey in spells)
            {
                _spellLookup.Add(spellKey.key, spellKey.GetSpell());
            }
        }

        public ISpell GetSpell(string spellName)
        {
            if (!_spellLookup.TryGetValue(spellName, out ISpell spell))
            {
                throw new SpellMissingException(spellName);
            }
            return spell;
        }
    }
}