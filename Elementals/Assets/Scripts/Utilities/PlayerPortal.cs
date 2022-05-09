using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerPortal : MonoBehaviour
{
   
    public PlayerDetectionTrigger portalTrigger;

    public bool useWorldSpace;
    public Vector2 teleportPosition;

    private void Awake()
    {
        portalTrigger.onPlayerDetectionChanged.AddListener(OnDetectPlayer);
    }

    private void OnDestroy()
    {
        portalTrigger.onPlayerDetectionChanged.RemoveListener(OnDetectPlayer);
    }

    void OnDetectPlayer(bool isDetected)
    {
        if (isDetected) {
            
        }
    }
    
    public Vector2 TeleportPosition => useWorldSpace ? teleportPosition : transform.TransformPoint(teleportPosition);
}


#if UNITY_EDITOR


[CustomEditor(typeof(PlayerPortal))]
public class PlayerPortalEditor : Editor
{
    private void OnSceneGUI()
    {
        PlayerPortal portal = target as PlayerPortal;
        if (portal.portalTrigger == null) {
            var go = new GameObject("Portal PlayerDetectionTrigger");
            go.transform.parent = portal.transform;
            go.transform.localPosition = Vector3.zero;
            var coll = go.AddComponent<CircleCollider2D>();
            coll.isTrigger = true;
            portal.portalTrigger = go.AddComponent<PlayerDetectionTrigger>();
        }
        var pos = portal.TeleportPosition;
        var useWorldSpace = portal.useWorldSpace;
        var newPos = pos;

        var startPosition = portal.portalTrigger.transform.position;
        var endPosition = portal.TeleportPosition;
        
        Handles.color = Color.green;
        Handles.DrawLine(startPosition, endPosition);
        Handles.DrawWireDisc(endPosition, Vector3.forward, 0.5f);
        Handles.color = Color.Lerp(Color.green, Color.white, 0.5f);
        Handles.DrawWireDisc(startPosition, Vector3.forward, 0.25f);
        
        if (!useWorldSpace) {
            var transform = portal.transform;
            newPos = transform.InverseTransformPoint(newPos);
        }
        EditorGUI.BeginChangeCheck();
        newPos = Handles.PositionHandle(newPos, Quaternion.identity);
        if (EditorGUI.EndChangeCheck()) {
            
        }
        
    }
}

#endif