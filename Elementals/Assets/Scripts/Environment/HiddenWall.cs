#define  DOTWEEN
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
#if DOTWEEN
using DG;
#endif
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Tilemaps;

#endif

namespace Environment
{
    public class HiddenWall : PlayerTriggerBase
    {
        [HideInInspector]
        [Tooltip("RGB values that will be passed into the unity event (alpha will be override)")]
        public Color wallColor = Color.white;

        [Range(0.15f, 1.4f)]
        [Tooltip("The number of seconds it takes for the wall to fade in/out")]
        public float duration = 1;
        
        [Range(0, 6), Tooltip("delay after player has exited trigger before the fades in again")]
        public float exitDelay = 1;
        
        
        [Header("Other Settings")]
        [Tooltip("If enabled all the SpriteRenderers that are childed to this object will be faded")]
        public bool fadeChildren = true;
        
        
        [Header("Extend Functionality")]
        [Tooltip("This event allows this component to be extended to other renderers such as a TileMapRenderer or a material")]
        public UnityEvent<Color> updateWallColor;
        
       
        

        private bool HasSpriteRenderers => _renderers != null && _renderers.Length > 0;

        private float _alpha = 1;
        private SpriteRenderer[] _renderers;
        private Coroutine _activeCoroutine;


        private void Awake()
        {
            _renderers = fadeChildren ?  GetComponentsInChildren<SpriteRenderer>() : GetComponents<SpriteRenderer>();
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

        private Tween fadeTween;
        void StopFading()
        {
            if(fadeTween!=null && fadeTween.IsPlaying())
                fadeTween.Kill();
        }
        void TriggerFadeIn()
        {
            StopFading();
            var remainingTime = duration - (duration * _alpha);
            fadeTween = GetFadeTween(1, remainingTime).SetDelay(exitDelay).Play();
        }
        void TriggerFadeOut()
        {
            StopFading();
            var remainingTime = duration - (duration * (1 - _alpha)); 
            fadeTween = GetFadeTween(0, remainingTime).Play();
        }

        private TweenerCore<float, float, FloatOptions> GetFadeTween(float target, float remainingTime)
        {
            return DOTween.To(() => _alpha, UpdateAlpha, target, remainingTime);
        }




        #endregion


        [SerializeField, HideInInspector]
        internal bool hidden = false;
        internal void HideWall()
        {
            hidden = true;
            Awake();
            UpdateAlpha(0);
        }

        internal void ShowWall()
        {
            hidden = false;
            UpdateAlpha(1);
        }
    }
    
    
    #if UNITY_EDITOR

    [CustomEditor(typeof(HiddenWall))]
    public class HiddenWallEditor : Editor
    {
        private SerializedProperty _propWallColor;

        private void OnEnable()
        {
            this._propWallColor  = serializedObject.FindProperty("wallColor");
            serializedObject.Update();
            var c = this._propWallColor.colorValue;
            c.a = 1;
            this._propWallColor.colorValue = c;
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var wall = target as HiddenWall;
            if(!Application.isPlaying)
                DrawWallButtons(wall);
            
            
            DrawWallColorField(wall);

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }

        /// <summary>
        /// ensures that tilemap color and wall color are the same when script is used with tilemap
        /// </summary>
        /// <param name="wall"></param>
        private void DrawWallColorField(HiddenWall wall)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_propWallColor);
            if (EditorGUI.EndChangeCheck())
            {
                wall.ShowWall();
                var tilemap = wall.GetComponent<Tilemap>();
                if (tilemap != null) tilemap.color = _propWallColor.colorValue;
                EditorGUI.EndChangeCheck();
            }
            return;
            var tm = wall.GetComponent<Tilemap>();
            if (tm != null)
            {
                if (tm.color != wall.wallColor)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Box(
                            "The tilemap's color will be overwritten by the wall color. If you have modified the tilemap color, copy the color to the wall color field.",
                            EditorStyles.helpBox);
                        if (GUILayout.Button("Fix", GUILayout.MaxWidth(35)))
                        {
                            _propWallColor.colorValue = tm.color;
                        }
                    }
                    GUILayout.EndHorizontal();
                }

                EditorGUI.BeginChangeCheck();
                _propWallColor.colorValue = EditorGUILayout.ColorField("Wall Color", _propWallColor.colorValue = tm.color);
                if (EditorGUI.EndChangeCheck())
                {
                    tm.color = _propWallColor.colorValue;
                }
            }
            else
            {
                _propWallColor.colorValue = EditorGUILayout.ColorField("Wall Color", _propWallColor.colorValue);
            }
        }

        private void DrawWallButtons(HiddenWall wall)
        {
            Stack<Color> pColor = new Stack<Color>(), pbgColor = new Stack<Color>();
           
            void ReplaceGuiColors(Color color, Color bgColor)
            {
                pColor.Push(GUI.color);
                pbgColor.Push(GUI.backgroundColor);
                GUI.color = color;
                GUI.backgroundColor = bgColor;
            }
            void RevertGuiColors()
            {
                if(pColor.Count > 0)
                GUI.color = pColor.Pop();
                if(pbgColor.Count > 0)
                GUI.backgroundColor = pbgColor.Pop();
            }

            void SetColorsDisabled()
            {
                ReplaceGuiColors(Color.gray, Color.Lerp(Color.black, Color.gray, 0.5f));
            }

            Color Darken(Color c, float amount) => Color.Lerp(c, Color.black, Mathf.Clamp01(amount));
            Color Lighten(Color c, float amount) => Color.Lerp(c, Color.yellow, Mathf.Clamp01(amount));
            
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if(wall.hidden) SetColorsDisabled();
                    if (GUILayout.Button("Hide Wall", GUILayout.MinHeight(35)))
                    {
                        wall.HideWall();
                    }
                    if(wall.hidden) RevertGuiColors();
                    else SetColorsDisabled();
                    if (GUILayout.Button("Show Wall", GUILayout.MinHeight(35)))
                    {
                        wall.ShowWall();
                    }
                    if(!wall.hidden) RevertGuiColors();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                {
                    ReplaceGuiColors(Darken(GUI.color, 0.2f), Darken(GUI.backgroundColor, 0.2f));
                    if (GUILayout.Button("Hide All", GUILayout.MinHeight(15)))
                    {
                        SetAllWallsVisible(false);
                    }
                    if (GUILayout.Button("Show All", GUILayout.MinHeight(15)))
                    {
                        SetAllWallsVisible(true);
                    }
                    RevertGuiColors();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void SetAllWallsVisible(bool visible)
        {
            serializedObject.FindProperty("hidden").boolValue = !visible;
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