using System;
using System.Collections.Generic;

namespace Damage
{
    /// <summary>
    /// class is responsible for determining the damage multiplier when one element is damaged by another
    /// </summary>
    public static class ElementMatrix
    {
        static Dictionary<Element, Element> weaknessLookup = new Dictionary<Element, Element>();
        static Dictionary<Element, Element> resistanceLookup = new Dictionary<Element, Element>();

        private const float attackingResistantMultiplier = 0f;
        private const float attackingWeaknessMultiplier = 2f;
        private const float attackingSelfMultiplier = 0.5f;
        private const float normalMultiplier = 1f;
        static void AddElementPair(Element strong, Element weak)
        {
            weaknessLookup.Add(strong, weak);
            resistanceLookup.Add(weak, strong);
        }
        static ElementMatrix()
        {
            AddElementPair(Element.Water, Element.Fire);
            AddElementPair(Element.Thunder, Element.Water);
            AddElementPair(Element.Earth, Element.Thunder);
            AddElementPair(Element.Air, Element.Earth);
            AddElementPair(Element.Fire,Element.Air);
            
        }
        public static float getDamageModifier(Element receiver, Element damageElement)
        {
            var weakToAttacker = weaknessLookup[damageElement];
            var resistantToAttacker = resistanceLookup[damageElement];

            if (receiver == weakToAttacker) {
                return attackingWeaknessMultiplier;
            }

            if (receiver == resistantToAttacker) {
                return attackingResistantMultiplier;
            }

            if (receiver == damageElement) {
                return attackingSelfMultiplier;
            }

            return normalMultiplier;

            switch (receiver) {
                case Element.Fire:
                    
                    break;
                case Element.Water:
                    break;
                case Element.Earth:
                    break;
                case Element.Air:
                    break;
                case Element.Thunder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(receiver), receiver, null);
            }
            switch (receiver)
            {
                case Element.Fire:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 0.5f;
                        case Element.Water:
                            return 2;
                        case Element.Earth:
                            return 1;
                        case Element.Air:
                            return 0;
                        case Element.Thunder:
                            return 1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                    break;
                case Element.Water:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 0;
                        case Element.Water:
                            return 0.5f;
                        case Element.Earth:
                            return 1;
                        case Element.Air:
                            return 1;
                        case Element.Thunder:
                            return 2;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                    break;
                case Element.Earth:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 1;
                        case Element.Water:
                            return 1;
                        case Element.Earth:
                            return 0.5f;
                        case Element.Air:
                            return 0;
                        case Element.Thunder:
                            return 2;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                case Element.Air:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 0;
                        case Element.Water:
                            return 1;
                        case Element.Earth:
                            return 2;
                        case Element.Air:
                            return 0.5f;
                        case Element.Thunder:
                            return 1;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                case Element.Thunder:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 1f;
                        case Element.Water:
                            return 2f;
                        case Element.Earth:
                            return 1f;
                        case Element.Air:
                            return 1;
                        case Element.Thunder:
                            return 0.5f;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(receiver), receiver, null);
            }
            return 1;
        }   
    }
}