using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class OnDestroyEvent : MonoBehaviour
    {
        public UnityEvent onDestroyed;


        public void OnDestroy()
        {
            onDestroyed?.Invoke();
        }
    }
}