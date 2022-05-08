using System;
using UnityEngine;

namespace AI
{
    public class TestZoneAI : MonoBehaviour
    {
        public AIZone zone;

        
        

        public Transform testTarget;


        private void OnDrawGizmos()
        {
            if (testTarget == null) return;
        }
    }
    
}