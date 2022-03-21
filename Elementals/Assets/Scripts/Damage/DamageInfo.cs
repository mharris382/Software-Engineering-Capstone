namespace Damage
{
    public struct DamageInfo
    {

        public bool HasDamageBeenApplied { get; internal set; }
        
        public float DamageMultiplier { get; internal set; }

        /// <summary>
        /// the actual amount of damage that was dealt to the reciever
        /// </summary>
        public float Damage => RawDamage * DamageMultiplier; 
        
        /// <summary>
        /// the initial amount of damage (provided by the damage dealer) 
        /// </summary>
        public float RawDamage { get; set; }
        
        /// <summary>
        /// the type of elemental damage (provided by the damage dealer). 
        /// </summary>
        public Element Element { get; set; }
        
    }
}