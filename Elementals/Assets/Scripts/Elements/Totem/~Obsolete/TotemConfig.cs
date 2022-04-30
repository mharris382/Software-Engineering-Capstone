using System;
using UnityEngine;
using UnityEngine.Events;

namespace Elements.Totem
{
    [Serializable]
    public class TotemConfig
    {
        [Range(3, 20)] public float totemRadius = 5;
        [Range(5, 60)] public float rechargeDuration = 5;

        [Header("Events")]
        public UnityEvent<bool> onTotemActive;
    }
}