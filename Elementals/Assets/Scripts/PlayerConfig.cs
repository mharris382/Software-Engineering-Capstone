using System;
using Mono.Cecil;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        
        public bool allowEditInPlayMode;
        
        [Header("Stats")]
        public float playerMaxHealth = 10;
        public float playerMaxMana = 10;
        
        [Header("Movement")]
        [Range(1, 30)]
        public float moveSpeed = 10;
        
        public float jumpForce = 9;
        public float maxJumpTime = 4.5f;

        [Header("RigidBody")]
        public float gravityScale = 3;
        public float mass = 2;

        [Header("Mana Gathering")] 
        public float gatherRadius = 15;
        public float consumeRadius = .5f;
        public float gatherForce = 15;
        
        
        private static PlayerConfig _playerConfig;
        public static PlayerConfig Config
        {
            get
            {
                if (_playerConfig == null)
                {
                    _playerConfig = Resources.Load<PlayerConfig>("PlayerConfig");
                }
                return _playerConfig;
            }
        }
    }
    
    
#if UNITY_EDITOR

    public class PlayerConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var t = target as PlayerConfig;
            if (Application.isPlaying && !t.allowEditInPlayMode)
            {
                using (var scope = new EditorGUI.DisabledGroupScope(true))
                {
                    base.OnInspectorGUI();
                }
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
    
#endif
}

