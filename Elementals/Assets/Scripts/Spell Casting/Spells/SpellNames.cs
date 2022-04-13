namespace Spell_Casting.Spells
{
    public static class SpellNames
    {
        public const string FastAttackSpell = "Fast";
        public const string StrongAttackSpell = "Strong";


        public static string GetFastAttackSpellName(Element element)
        {
            return $"{SpellNames.FastAttackSpell}_{element.ToString()}";
        }
    }
}