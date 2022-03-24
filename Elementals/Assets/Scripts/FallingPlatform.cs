using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float duration = 1f;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float elasticity = 1f;
    [SerializeField] private float delay = 0.5f;
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

        rb.transform.DOPunchRotation(Vector3.forward, 1f, 10, 1f)
            .SetDelay(delay)
            .OnComplete(() => rb.isKinematic = false)
            .Play();

    }
}
