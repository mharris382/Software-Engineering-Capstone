using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using Elements;
using Elements.Utilities;
using UnityEditor;
#endif
using UnityEngine;

namespace AI
{
    public class AINavFollowerInjector : MonoBehaviour
    {
        public float Radius = 5;
        public Transform RadiusCenter;
        
        private NavigationFollowerEnemyAI[] childNavFollowers;

        public NavigationFollowerEnemyAI[] FollowerEnemyAis => childNavFollowers;
        void Awake()
        {
            childNavFollowers = GetComponentsInChildren<NavigationFollowerEnemyAI>();
            foreach (var navigationFollowerEnemyAI in childNavFollowers) {
                navigationFollowerEnemyAI.Radius = Radius;
                if(RadiusCenter!=null) navigationFollowerEnemyAI.AssignRadiusCenter(RadiusCenter);
                else navigationFollowerEnemyAI.ClearRadiusCenter();
            }
        }
        
        public Vector2 centerPosition => RadiusCenter != null ? RadiusCenter.position : transform.position;

        public bool HasRadiusCenter()
        {
            return RadiusCenter != null;
        }

        public void ClearRadiusCenter()
        {
            RadiusCenter = null;
        }

        public void AssignRadiusCenter(Transform goTransform)
        {
            RadiusCenter = goTransform;
        }

       

        public void RefreshChildren()
        {
            childNavFollowers = GetComponentsInChildren<NavigationFollowerEnemyAI>();
        }
    }

#if UNITY_EDITOR

    [CanEditMultipleObjects]
    [CustomEditor(typeof(AINavFollowerInjector))]
    public class AINavFollowerInjectorEditor : Editor
    {
        

        private void OnSceneGUI()
        {
            var follower = target as AINavFollowerInjector;
            if (follower == null) return;
            
           
            DrawHandlesForAI(follower);
            if (follower.FollowerEnemyAis == null || follower.FollowerEnemyAis.Length == 0) {
                follower.RefreshChildren();
            }

            if (follower.FollowerEnemyAis == null || follower.FollowerEnemyAis.Length == 0) return;
            var pos = follower.centerPosition;
            var c = Color.red;
            Handles.color = c;
            Handles.DrawSolidDisc(pos, Vector3.forward, 0.25f);
            foreach (var followerEnemyAi in follower.FollowerEnemyAis) {
                var followerPos = followerEnemyAi.transform.position;
                var followerElementInjector = followerEnemyAi.GetComponentInParent<ElementInjector>();
                
                if (followerElementInjector != null) {
                    var element = followerElementInjector.element;
                    c = ElementColorPalettes.GetColor(element);
                }
                c.a *= 0.5f;
                Handles.color = c;
                Handles.DrawDottedLine(pos, followerPos, 0.1f);
            }
        }
        
        

        private void DrawHandlesForAI(AINavFollowerInjector follower, bool multiTarget =false)
        {
            Handles.color = Color.green;
            if ( Event.current.control) {
                SetRadiusCenter(follower);
            }
            else if (!multiTarget && Event.current.shift) {
                ClearRadiusCenter(follower);
            }
            if (follower.HasRadiusCenter()) {

                float radius = follower.Radius;
                var center = follower.RadiusCenter.position;
                float newRadius = Handles.RadiusHandle(Quaternion.identity, center, radius);
                if (Mathf.Abs(newRadius - radius) > Mathf.Epsilon) {
                    follower.Radius = newRadius;
                    Undo.RecordObject(follower, "Change radius");
                }
            }
        }

        private void ClearRadiusCenter(AINavFollowerInjector follower)
        {
            if (follower.HasRadiusCenter()) {
                var  mousePos = Event.current.mousePosition;
                var scenePos = HandleUtility.GUIPointToWorldRay(mousePos).origin;
                var c = Color.red;
                var pos = follower.RadiusCenter.position;
                bool isSelecting = Vector2.Distance(scenePos, pos) < 1f;
                c.a = isSelecting ? 0.5f : 0.2f;
                if (Event.current.type == EventType.MouseDown&&isSelecting) {
                    Undo.RecordObject(follower, "Clear radius center");
                    follower.ClearRadiusCenter();
                    Undo.DestroyObjectImmediate(follower.RadiusCenter.gameObject);
                }
            }
        }

        private void SetRadiusCenter(AINavFollowerInjector follower)
        {
            var  mousePos = Event.current.mousePosition;
            var scenePos = HandleUtility.GUIPointToWorldRay(mousePos).origin;
            var c = Color.green;
            if (!follower.HasRadiusCenter()) {
          
                c.a = 0.5f;
                Handles.color = c;
               
                Handles.DrawSolidDisc(scenePos, Vector3.forward, 0.1f);
                if (Event.current.type == EventType.MouseDown) {
                    c.a = 1;
                    Handles.color = c;
                    var go = new GameObject("RadiusCenter");
                    go.transform.position = scenePos;
                    go.hideFlags = HideFlags.HideInHierarchy;
                    follower.AssignRadiusCenter(go.transform);
                    Undo.RegisterCreatedObjectUndo(go, "Assign radius center");
                }
            }
            else {
                c.a = 1;
                Handles.DrawSolidDisc(scenePos, Vector3.forward, 0.1f);
                var t = follower.RadiusCenter;
                t.position = scenePos;
                Undo.RecordObject(follower.RadiusCenter, "Change radius center");
            }
        }
    }
#endif
}