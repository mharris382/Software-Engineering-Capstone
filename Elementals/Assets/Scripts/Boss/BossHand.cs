using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class BossHand : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve punchLayerWeightCurve = AnimationCurve.EaseInOut(0,0, 1, 1);


    [SerializeField]
    private Transform follow;


    [FormerlySerializedAs("followRate")]
    [SerializeField] private float followSpeed = 0.1f;

    private float maxDistPerSec = 1;

    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float followDistance = 1;
    [SerializeField] private AnimationCurve followCurve = AnimationCurve.EaseInOut(0,0,1,1);

    float GetSpeed(float distanceToTarget, bool clamp = false)
    {
        if(clamp)
            distanceToTarget = Mathf.Clamp(distanceToTarget, 0, followDistance);
        var t = Mathf.InverseLerp(0, followDistance, distanceToTarget);
        t = followCurve.Evaluate(t);
        return Mathf.Lerp(minSpeed, maxSpeed, t);
    }
    
    
    [SerializeField] private int punchLayer = 5;
    private float _punching;
    private Animator _anim;
    private float _grabMaster = 1;
    private float[] _fingers = new float[4]{0, 0, 0, 0};

    private bool _isClasped;
    private static readonly int Clasped = Animator.StringToHash("IsClasped");
    private static readonly int FlippingTheBird = Animator.StringToHash("FlippingTheBird");

    public bool IsClasped
    {
        get => Anim.GetBool(Clasped);
        set => Anim.SetBool(Clasped, value);
    }

    public Transform Follow
    {
        get => follow;
        set => follow = value;
    }
    private Animator Anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }
    public float GrabMaster
    {
        get
        {
            return _grabMaster;
        }
        set
        {
            value = Mathf.Clamp01(value);
            if (Math.Abs(_grabMaster - value) > 0.001f)
            {
                _grabMaster = value;
                UpdateGrab();
            }
        }
    }

    public float FlipTheBird
    {
        get => Anim.GetFloat(FlippingTheBird);
        set => Anim.SetFloat(FlippingTheBird, Mathf.Clamp01(value));
    }

    public float Punching
    {
        get
        {
            return _punching;
        }
        set
        {
            value = Mathf.Clamp01(value);
            if (_punching != value)
            {
                _punching = value;
                UpdatePunching();
            }
        }
    }

    public float F1
    {
        get => _fingers[0];
        set => UpdateFinger(0, value);
    }
    public float F2
    {
        get => _fingers[1];
        set => UpdateFinger(1, value);
    }
    public float F3
    {
        get => _fingers[2];
        set => UpdateFinger(2, value);
    }
    public float T
    {
        get => _fingers[3];
        set => UpdateFinger(3, value);
    }


    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void UpdateGrab()
    {
        Anim.SetFloat("F1", F1 * GrabMaster);
        Anim.SetFloat("F2", F2 * GrabMaster);
        Anim.SetFloat("F3", F3 * GrabMaster);
        Anim.SetFloat("T", T * GrabMaster);
    }
    
    void UpdateFinger(int finger, float value)
    {
        _fingers[finger] = value;
        UpdateGrab();
    }
    
    
    void UpdateHand()
    {
        UpdateGrab();
        UpdatePunching();
    }
    

    private void UpdatePunching()
    {
        _anim.SetLayerWeight(punchLayer, punchLayerWeightCurve.Evaluate(Punching));
    }
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        UpdateHand();
        if (follow != null)
        {
            FollowPoint(follow.position);
        }
    }

    private void FollowPoint(Vector3 followPosition)
    {
        var targetPos = followPosition;
        var curPos = transform.position + localDelta;
        
        if (followSpeed <= 0)
            transform.position = targetPos;
        else
        {
            var speed = GetSpeed(Vector2.Distance(targetPos, curPos), true);
           var newPos =Vector3.Slerp(curPos, targetPos, speed*Time.deltaTime);
#if false
            var maxDistPerFrame = 1;
            if ((transform.position - newPos).magnitude > maxDistPerFrame)
            {
                newPos = (transform.position - newPos).normalized * maxDistPerFrame;
            }
#endif
            transform.position = newPos;
        }
    }

    private void OnAnimatorMove()
    {
        localDelta = Anim.deltaPosition;
        // var followPos = follow == null ? Vector3.zero : follow.position;
        // var pos = followPos + movedPos;
        // Position = pos;
    }

    private Vector3 localDelta;
}
