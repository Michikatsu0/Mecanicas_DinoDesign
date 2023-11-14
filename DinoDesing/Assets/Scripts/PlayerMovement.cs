using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if(input > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input < 0)
        {
            spriteRenderer.flipX = false;
        }
        //else if (input == 0f)
        //{
        //    spriteRenderer.flipX = true;
        //}
        if (Input.GetButton("Jump"))
        {
            playerRb.velocity = Vector2.up * jumpForce;
        }

    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }
}
