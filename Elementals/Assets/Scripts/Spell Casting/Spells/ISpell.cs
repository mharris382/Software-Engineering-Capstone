using UnityEngine;

namespace Spell_Casting.Spells
{
    public interface ISpell
    {
        bool CastSpell(GameObject caster, Vector2 position, Vector2 direction);
        float ManaCost {get;}
    }

    public class Spell : MonoBehaviour
    {
        public string key;
    }
}