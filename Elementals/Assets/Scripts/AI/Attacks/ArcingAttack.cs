using System;
using UnityEngine;

using DG.Tweening;

namespace DefaultNamespace.Attacks
{
    public class ArcingAttack : MonoBehaviour
    {
        [SerializeField] private float jumpPower = 1;
        [SerializeField] private float jumpTime = 1;
        [SerializeField] private float linearSpeed = 10;
        [SerializeField]
        private Rigidbody2D attackPrefab;

        public Transform Target
        {
            set
            {
                if (value != null)
                {
                    if (targetPoint == null)
                    {
                        targetPoint = new GameObject("Target").transform;
                    }
                    targetPoint.SetParent(value, false);
                }
            }
            get
            {
                if (targetPoint == null)
                {
                    targetPoint = new GameObject("Target").transform;
                }

                return targetPoint;
            }
        }
        private Transform targetPoint;
        private Sequence _jumpTweener;
        [SerializeField] private Ease jumpEase;
        private void Awake()
        {
            if (targetPoint == null)
            {
                targetPoint = new GameObject("Target").transform;
            }
        }

        public void DoAttack()
        {
            var body = Instantiate(attackPrefab, transform.position, transform.rotation);
            TriggerJump(body, () =>
            {
                Destroy(body.gameObject);
            });
        }   


        void TriggerJump(Rigidbody2D body, Action onCompleted)
        {
            var endPos = Target.position;
            var startPos = transform.position;
            float dist = Vector2.Distance(endPos, startPos);
            float time = dist / linearSpeed;
            time = Mathf.Min(time, jumpTime);
           _jumpTweener =  body.DOJump(endPos, jumpPower, 1, time, false)
               .SetEase(jumpEase)
               .OnComplete(() => onCompleted?.Invoke())
               .Play();
        }
    }
}