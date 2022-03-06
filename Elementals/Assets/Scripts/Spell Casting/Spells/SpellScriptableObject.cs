using UnityEngine;

namespace Spell_Casting.Spells
{
    [CreateAssetMenu(menuName = "Create SpellScriptableObject", fileName = "SpellScriptableObject", order = 0)]
    public class SpellScriptableObject : ScriptableObject, ISpell
    {
        public void CastSpell(GameObject caster, Vector2 position, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public float ManaCost { get; }
    }
}