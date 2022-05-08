using UnityEngine;

namespace AI
{
    public class GuardEnemyAI : MonoBehaviour
    {
        public EngagementZone engageZone;
        public EngagementZone disengageZone;
        
        public Transform testObject;


        private void OnDrawGizmos()
        {
            engageZone.DrawGizmos(Color.Lerp(Color.magenta, Color.red, 0.45f));
            if (testObject != null)
            {
                engageZone.DrawTestObject(testObject);
            }
        }
    }

    
}