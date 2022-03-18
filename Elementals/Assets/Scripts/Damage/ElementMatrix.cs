using System;

namespace Damage
{
    public static class ElementMatrix
    {
        public static float getDamageModifier(Element receiver, Element damageElement)
        {
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
                            return 1.25f;
                        case Element.Air:
                            return 0.25f;
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
                            return 0.25f;
                        case Element.Water:
                            return 0.5f;
                        case Element.Earth:
                            return 1.25f;
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
                            return 1.5f;
                        case Element.Water:
                            return 1.5f;
                        case Element.Earth:
                            return 0.5f;
                        case Element.Air:
                            return 1.25f;
                        case Element.Thunder:
                            return 0.25f;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                case Element.Air:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 1f;
                        case Element.Water:
                            return 1.25f;
                        case Element.Earth:
                            return 0.5f;
                        case Element.Air:
                            return 0.25f;
                        case Element.Thunder:
                            return 2;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(damageElement), damageElement, null);
                    }
                case Element.Thunder:
                    switch (damageElement)
                    {
                        case Element.Fire:
                            return 1f;
                        case Element.Water:
                            return 0.5f;
                        case Element.Earth:
                            return 2f;
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