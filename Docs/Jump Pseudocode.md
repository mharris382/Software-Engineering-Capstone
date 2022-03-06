   
```cs
 private void Update()
    {
        State.IsGrounded = CheckIsGrounded();

        if (IsInteracting) {
            IsJumping = false;
            return;
        }

        if (IsGrounded && JumpPressed) {
            IsJumping = true;
            _jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (IsJumping) {
            if (JumpHeld && _jumpTimeCounter > 0.0f) {
                rb.velocity = Vector2.up * jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else {
                IsJumping = false;
            }
        }
    }
```