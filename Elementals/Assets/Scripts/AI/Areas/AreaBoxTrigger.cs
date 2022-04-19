using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaBoxTrigger : MonoBehaviour
{
    private BoxCollider2D _box;


    
    private void Awake()
    {
        _box = GetComponent<BoxCollider2D>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
                
    }
}
