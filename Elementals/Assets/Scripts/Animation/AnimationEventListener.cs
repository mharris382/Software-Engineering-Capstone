using System;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventListener : MonoBehaviour
    {
        private Animator _anim;
        private CharacterState _state;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _state = GetComponentInParent<CharacterState>();
            Debug.Assert(_state != null, $"{name} is missing a CharacterState component in parent");
        }
    }
}