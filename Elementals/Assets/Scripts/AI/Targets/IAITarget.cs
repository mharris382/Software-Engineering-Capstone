using System;
using UnityEngine;

namespace AI
{
    public interface IAITarget
    {
        public bool HasTarget { get; }
        public Vector2 TargetPosition{get;}
        public Vector2 TargetVelocity{get;}
        
        public Element TargetElement { get; }
    }
}