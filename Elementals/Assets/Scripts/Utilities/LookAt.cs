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
            Vector2 dir = (target.position - aimTransform.position).normalized;
            var angle = Vector2.SignedAngle(Vector2.right, dir);
            var rot = Quaternion.Euler(0, 0, angle);
            aimTransform.rotation = rot;
            //aimTransform.LookAt(target,Vector3.up);
        }
    }
}