using UnityEngine;

namespace DefaultNamespace
{
    public class UnlimitedMana : MonoBehaviour, IManaSource
    {
        public float CurrentValue => float.MaxValue;

        public float MaxValue => float.MaxValue;

        public bool HasMana(float amount)
        {
            return true;
        }

        public void AddMana(float amount)
        {
         
        }

        public void RemoveMana(float amount)
        {
         
        }
    }
}