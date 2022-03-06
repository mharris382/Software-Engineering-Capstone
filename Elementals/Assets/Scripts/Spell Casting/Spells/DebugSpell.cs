using UnityEngine;

namespace Spell_Casting.Spells
{
    public class DebugSpell : MonoBehaviour, ISpell
    {
        public void CastSpell(GameObject caster, Vector2 position, Vector2 direction)
        {
            Debug.Log(message);
        }

        public float ManaCost
        {
            get { return manaCost; }
        }

        [SerializeField]
        private float manaCost = 1;

        [SerializeField] private string message = "Spell was cast";
    }
}