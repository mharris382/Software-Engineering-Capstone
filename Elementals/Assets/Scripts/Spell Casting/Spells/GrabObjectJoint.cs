using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spell_Casting.Spells
{
    public class GrabObjectJoint : MonoBehaviour
    {
        public SpringJoint2D joint;
        
        
    }

    public class ObjectGrabber : MonoBehaviour
    {
        public GrabObjectDetector grabDetector;
        public GrabObjectJoint grabJoint;
    }

    public class GrabObjectSpell : MonoBehaviour
    {
        public ObjectGrabber grabber;



    }

    public class GrabObjectDetector : MonoBehaviour
    {
        private HashSet<Rigidbody2D> _grabbables = new HashSet<Rigidbody2D>();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.attachedRigidbody == null) return;
        }
    }

    
    /// <summary>
    /// conditional spell, it is combined with a grabbed object that is being held
    /// </summary>
    public abstract class GrabbedObjectSpell : MonoBehaviour
    {
        
    }
}