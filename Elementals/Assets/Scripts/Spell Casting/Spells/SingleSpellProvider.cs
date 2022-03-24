using UnityEngine;

namespace Spell_Casting.Spells
{
    public class SingleSpellProvider : MonoBehaviour, ISpellProvider
    {
        private ISpell _spell;
        private void Awake()
        {
            _spell = GetComponent<ISpell>();
            if (_spell == null)
                _spell = GetComponent<ISpell>();
            Debug.Assert(_spell != null, "<b><color=red>Single Spell Provider is missing a component that implements ISpell!</color></b>" +
                                         "\n<b>AddComponent->Spells</b> to browse spell that have been implemented.",this);
        }

        public ISpell GetSpell(string spellName)
        {
            Debug.Assert(_spell != null, "<b><color=red>Single Spell Provider is missing a component that implements ISpell!</color></b>" +
                                         "\n<b>AddComponent->Spells</b> to browse spell that have been implemented.",this);
            return _spell;
        }
    }
    
    
}