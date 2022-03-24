using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 2f);
        }

    }

    private void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
