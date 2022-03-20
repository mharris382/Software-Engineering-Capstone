using System;
using System.Collections;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BlinkSprite : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private Coroutine _blinkRoutine;
        
        [SerializeField]
        private float minBlinksPerSecond = .2f;
        
        [SerializeField]
        private float maxBlinksPerSecond = 50;

        [SerializeField] private AnimationCurve blinkCurve = AnimationCurve.Linear(0,0,1,1);
        [SerializeField]
        private float totalDuration = 3;
        
        [SerializeField]
        private float blinkDuration = 0.01f;

        [SerializeField] 
        private Color offColor = Color.clear;
        
        [SerializeField] 
        private bool endsOff = true;
        
        private Color _onColor;

        
        
        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _onColor = _sr.color;
        }

        private void OnEnable()
        {
            StartBlink();
        }

        public void StartBlink()
        {
            if(_blinkRoutine != null)StopCoroutine(_blinkRoutine);
            _blinkRoutine = StartCoroutine(Blink());
        }

        IEnumerator Blink()
        {
            void OnBlink(bool isBlinking)
            {
                _sr.color = isBlinking ? _onColor : offColor;
            }
            bool done = false;
            yield return Blink(OnBlink, totalDuration, blinkDuration, maxBlinksPerSecond, minBlinksPerSecond);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">total time that coroutine takes</param>
        /// <param name="maxSpeed">fastest speed that sprite blinks at</param>
        /// <param name="minSpeed"></param>
        /// <returns></returns>
        IEnumerator Blink(Action<bool> onBlink, float duration, float blinkDuration, float maxBPS, float minBPS)
        {
            bool on = true;
            bool isBlinking = false;

            #region Blink Methods

            void BlinkOn()
            {
                isBlinking = true;
                onBlink?.Invoke(true);
            }
            void BlinkOff()
            {
                isBlinking = false;
                onBlink?.Invoke(false);
            }
            void BlinkEnd()
            {
                onBlink?.Invoke(!endsOff);
            }

            #endregion

            // float totalTime = 0;
            // while (totalTime < duration)
            // {
            //     WaitForSeconds WaitUntilNextBlink()
            //     {
            //         var curBPs = Mathf.Lerp(minBPS, maxBPS, blinkCurve.Evaluate(totalTime/duration));
            //         var timeUntilNextBlink = (1 / curBPs);
            //         totalTime = timeUntilNextBlink;
            //         Debug.Log($"{timeUntilNextBlink}s to blink. Total");
            //         var wfs = new WaitForSeconds(timeUntilNextBlink);
            //         return wfs;
            //     }
            //     WaitForSeconds WaitForBlink()
            //     {
            //         totalTime += blinkDuration;
            //         var wfs = new WaitForSeconds(blinkDuration);
            //         return wfs;
            //     }
            //    yield return WaitUntilNextBlink();
            //     BlinkOn();
            //     
            // }
            for (float t = 0; t < 1; t+=0)
            {
                yield return WaitUntilNextBlink();;
                BlinkOn();
            
                yield return WaitForBlink();
                BlinkOff();
                
                #region [Wait Methods]

                WaitForSeconds WaitForBlink()
                {
                    t += blinkDuration / duration;
                    var wfs = new WaitForSeconds(blinkDuration);
                    return wfs;
                }
                WaitForSeconds WaitUntilNextBlink()
                {
                    var curBPs = Mathf.Lerp(maxBPS, minBPS, blinkCurve.Evaluate(t));
                    var timeUntilNextBlink = (1 / curBPs);
                    t += timeUntilNextBlink / duration;
                    var wfs = new WaitForSeconds(timeUntilNextBlink);
                    return wfs;
                }

                    #endregion
                yield return null;
            }
            BlinkEnd();
        }
    }
}