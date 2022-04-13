using UnityEngine;
using UnityEngine.Events;

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

    
    public struct SpellInfo
    {
        public GameObject caster { get; set; }
        public GameObject SpellInstance { get; set; }
        public Vector2 CastPosition { get; set; }
        public Vector2 CastDirection { get; set; }

        public float CastTime
        {
            get;
            set;
        }
    }

   
}