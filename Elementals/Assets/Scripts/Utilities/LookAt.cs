using System;
using UnityEngine;

namespace Utilities
{
    public class LookAt : MonoBehaviour
    {
        public Transform target;
        public Transform aimTransform;

        private void Update()
        {
            aimTransform.LookAt(target,Vector3.up);
        }
    }
}