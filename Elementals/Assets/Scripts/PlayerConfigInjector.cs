using System;
using UnityEngine;

namespace Configs
{
    public class PlayerConfigInjector : MonoBehaviour
    {
        public bool updateConfigInPlayMode;
        
        private HealthState _health;
        private ManaState _mana;
        private CharacterMove _move;
        private Rigidbody2D _rb;
        private ManaGatherer _gatherer;
        private ManaFInder _finder;

        private void Awake()
        {
           _health = GetComponentInChildren<HealthState>();
           _mana = GetComponentInChildren<ManaState>();
           _move = GetComponent<CharacterMove>();
           _rb = GetComponent<Rigidbody2D>();
           _gatherer = GetComponentInChildren<ManaGatherer>();
           _finder = GetComponentInChildren<ManaFInder>();
        }

        private void Start()
        {
            ApplyConfig();
#if UNITY_EDITOR
            var config = PlayerConfig.Config;
            if (config.allowEditInPlayMode)
            {
                InvokeRepeating("ApplyConfig", 0.0f, 0.1f);
            }
#endif
        }

        void ApplyConfig()
        {
            var config = PlayerConfig.Config;
            _health.MaxValue = config.playerMaxHealth;
            _mana.MaxValue = config.playerMaxMana;

            _move.jumpTime = config.maxJumpTime;
            _move.jumpVert = config.jumpForce;
            _move.moveSpeed = config.moveSpeed;

            _rb.gravityScale = config.gravityScale;
            _rb.mass = config.mass;

            _gatherer.ConsumeRadius = config.consumeRadius;
            _gatherer.ForceStrength = config.gatherForce;
            _finder.GetComponent<CircleCollider2D>().radius = config.gatherRadius;
        }
    }
}