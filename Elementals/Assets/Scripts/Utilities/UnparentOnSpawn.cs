using System;
using UnityEngine;

namespace Utilities
{
    public class UnparentOnSpawn : MonoBehaviour
    {
        private void Start()
        {
            transform.SetParent(null);
        }
    }
}