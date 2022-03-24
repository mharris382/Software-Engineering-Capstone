using System;
using UnityEngine;

namespace Spell_Casting.Spells
{
    public interface ISpellProvider
    {
        ISpell GetSpell(string spellName);
    }

    public class PlayerSpellProvider : MonoBehaviour, ISpellProvider
    {
        public SpellKey[] spells;
        public ElementContainer activeElement;
        
        
        [System.Serializable]
        public class SpellKey
        {
            public string key;
            public UnityEngine.Object spell;

            public bool Validate()
            {
                return GetSpell() != null;
            }
            public ISpell GetSpell()
            {
                if (spell is GameObject)
                {
                    return ((GameObject) spell).GetComponent<ISpell>();
                }
                return spell as ISpell;
            } 
        }
        
        public ISpell GetSpell(string spellName)
        {
            spellName = spellName + "_" + activeElement.Element;
            for (int i = 0; i < spells.Length; i++)
            {
                if (spells[i].key == spellName)
                {
                    if (spells[i].Validate() == false)
                    {
                        Debug.LogError(spellName + " missing valid spell", this);
                    }
                    var spell = spells[i].GetSpell();
                    return spell;
                }
            }
            throw new SpellMissingException(spellName);
        }
        
    }


    public class SpellMissingException : Exception
    {
        public SpellMissingException(string message) : base($"Spell {message} was not found!")
        {
        }
    }
}