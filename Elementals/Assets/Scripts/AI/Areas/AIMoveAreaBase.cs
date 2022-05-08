using System;
using UnityEngine;

namespace AI
{
    public abstract class AIMoveAreaBase : MonoBehaviour
    {
        public Vector2 GetAIMovementDestination(IAIState state) => GetAIMovementDestination(state, Time.deltaTime);
        public abstract Vector2 GetAIMovementDestination(IAIState state, float dt);
    }
}