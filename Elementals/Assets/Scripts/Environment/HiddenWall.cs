using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
    
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Environment
{
    public class HiddenWall : PlayerTriggerBase
    {
        [Tooltip("The number of seconds it takes for the wall to fade in/out")]
        public float duration = 3;
       
        [Tooltip("curve to apply easing to the animation")]
        public AnimationCurve fadeEasing = AnimationCurve.EaseInOut(0,0, 1,1);

        [Range(0, 6), Tooltip("delay after player has exited trigger before the fades in again")]
        public float exitDelay = 1;
        
        
        [Header("Other Settings")]
        [Tooltip("If enabled all the SpriteRenderers that are childed to this object will be faded")]
        public bool fadeChildren = true;
        
        
        [Header("Extend Functionality")]
        [Tooltip("This event allows this component to be extended to other renderers such as a TileMapRenderer or a material")]
        public UnityEvent<Color> updateWallColor;
        
        [Tooltip("RGB values that will be passed into the unity event (alpha will be override)")]
        public Color wallColor = Color.white;

        

        private bool HasSpriteRenderers => _renderers != null && _renderers.Length > 0;

        private float _alpha = 1;
        private SpriteRenderer[] _renderers;
        private Coroutine _activeCoroutine;


        private void Awake()
        {
            _renderers = fadeChildren ?  GetComponentsInChildren<SpriteRenderer>() : GetComponents<SpriteRenderer>();
            ValidateAnimationCurve();
            UpdateAlpha(1);
        }
    
        protected override void OnPlayerEnter(GameObject player) => TriggerFadeOut();
        protected override void OnPlayerExit(GameObject player) => TriggerFadeIn();

        #region [Update Color]

        private void UpdateAlpha(float alpha)
        {
            _alpha = alpha;
            var c = Color.white;
            c.a =  Mathf.Clamp01(alpha);
            ApplyColor(c);
        }

        private void ApplyColor(Color c)
        {
            UpdateSpriteColors(c);
            updateWallColor?.Invoke(c*wallColor);
        }
        
        void UpdateSpriteColors(Color c)
        {
            if (!HasSpriteRenderers) return;
            foreach (var spriteRenderer in _renderers)
            {
                if (spriteRenderer == null) continue;
                spriteRenderer.color = c;
            }
        }

        #endregion

        #region [Fade Animation]

        void StopFading()
        {
            if (_activeCoroutine != null) StopCoroutine(_activeCoroutine);
        }
        void TriggerFadeIn()
        {
            StopFading();
            _activeCoroutine = StartCoroutine(FadeWallIn());
        }
        void TriggerFadeOut()
        {
            StopFading();
            _activeCoroutine = StartCoroutine(FadeWallOut());
        }

        IEnumerator FadeWallIn()
        {
            if(exitDelay > 0)
                yield return new WaitForSeconds(exitDelay);
            
            ValidateAnimationCurve();
            float remainingTime = duration - (duration * _alpha);
            while (remainingTime > 0)
            {
                float newAlpha = 1 - fadeEasing.Evaluate(remainingTime / duration);
                UpdateAlpha(newAlpha);
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            UpdateAlpha(1);
        }

        IEnumerator FadeWallOut()
        {
            ValidateAnimationCurve();
            
            float remainingTime = duration - (duration * (1 - _alpha));
            while (remainingTime > 0)
            {
                float newAlpha = fadeEasing.Evaluate(remainingTime / duration);
                UpdateAlpha(newAlpha);
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            UpdateAlpha(0);
        }
        
        

        #endregion

        void ValidateAnimationCurve()
        {
            Debug.Assert(fadeEasing.Evaluate(1)>=1, "ConfigERROR(FadingWall)\n Assertion Failed: fadeEasing.Evaluate(1)=>1",this);
            Debug.Assert(fadeEasing.Evaluate(0)<=0, "ConfigERROR(FadingWall)\n Assertion Failed: fadeEasing.Evaluate(0)=>0",this);
        }


        
        
        internal void HideWall()
        {
            Awake();
            UpdateAlpha(0);
        }

        internal void ShowWall()
        {
            UpdateAlpha(1);
        }
    }
    
    
    #if UNITY_EDITOR

    [CustomEditor(typeof(HiddenWall))]
    public class HiddenWallEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var wall = target as HiddenWall;
            DrawWallButtons(wall);
            base.OnInspectorGUI();
        }

        private static void DrawWallButtons(HiddenWall wall)
        {
            EditorGUILayout.BeginVertical();
            {
                if (GUILayout.Button("Hide Wall", GUILayout.MinHeight(35)))
                {
                    wall.HideWall();
                }

                if (GUILayout.Button("Show Wall", GUILayout.MinHeight(35)))
                {
                    wall.ShowWall();
                }

                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Hide All", GUILayout.MinHeight(15)))
                    {
                        SetAllWallsVisible(false);
                    }

                    if (GUILayout.Button("Show All", GUILayout.MinHeight(15)))
                    {
                        SetAllWallsVisible(true);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private static void SetAllWallsVisible(bool visible)
        {
            var walls = FindObjectsOfType<HiddenWall>();
            foreach (var wall in walls)
            {
                if (visible) wall.ShowWall();
                else wall.HideWall();
            }
        }
    }
    
    #endif
}