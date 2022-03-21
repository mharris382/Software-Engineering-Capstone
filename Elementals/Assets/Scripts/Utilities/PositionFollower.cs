using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PositionFollower : MonoBehaviour
{

    [SerializeField]
    private Transform follow;

    public Transform Follow
    {
        get => follow;
        set => follow = value;
    }


    private void Update()
    {
        if (follow == null) return;
        transform.position = follow.position;
    }
}
