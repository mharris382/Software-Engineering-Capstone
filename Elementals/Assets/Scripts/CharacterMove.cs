using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMove : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float moveSpeed = 10f;
    public float jumpVert = 10f;
    public LayerMask groundMask;
    
    public float HorizontalMovement { get; set; }
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        var hit = Physics2D.Raycast(transform.position, Vector2.down, distance: 0.5f, groundMask);
        isGrounded = hit;

        if (isGrounded) 
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb.AddForce(Vector2.up * jumpVert, ForceMode2D.Impulse);
            }
        }
    }

    // Based on Physics Updates
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(HorizontalMovement, _rb.velocity.y);
    }

}
