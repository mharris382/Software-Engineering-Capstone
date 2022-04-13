using Spell_Casting.Spells;
using UnityEngine;
using UnityEngine.Events;

public class SpellEventListener : MonoBehaviour
{
    public UnityEvent<SpellInfo> SpellWasCast;
}