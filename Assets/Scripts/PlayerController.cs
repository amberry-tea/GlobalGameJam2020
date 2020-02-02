﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private float horiVelocity = 0.0f;  // Horizontal Velocity. Set by player movement.
    public Animator animator;
    public float speed;
    public float jumpHeight;
    private bool hasntJumped;

    public float fallMultiplier;
    public float lowJumpMultipler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    void Update()
    {
        animator.SetFloat("VertSpeed", rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        horiVelocity = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horiVelocity * speed, rb.velocity.y);
        if (hasntJumped)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                hasntJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        hasntJumped = true;
    }
}