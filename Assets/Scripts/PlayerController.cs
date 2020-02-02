using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
	private SfxPlayer sfxPlayer;
    private float horiVelocity = 0.0f;  // Horizontal Velocity. Set by player movement.
    public Animator animator;
    public float speed;
    public float jumpHeight;
    private bool hasntJumped;
    private bool hasntJumpedInAir;
    private bool isGrounded;
    private int jumps;

    public float fallMultiplier;
    public float lowJumpMultipler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        jumps = 2;
        hasntJumpedInAir = true;
        hasntJumped = true;
		sfxPlayer = GetComponent<SfxPlayer>() as SfxPlayer;
    }

    void Update()
    {
        animator.SetFloat("VertSpeed", rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        horiVelocity = Input.GetAxis("Horizontal");
		
		if(Mathf.Abs(horiVelocity) > 0 && hasntJumped)
		{
			sfxPlayer.PlaySFX("walk");
		} else {
			sfxPlayer.StopWalking();
		}
    }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jumps > 0)
            {
                hasntJumpedInAir = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hasntJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                hasntJumped = false;
            }
            else if (hasntJumpedInAir)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                hasntJumpedInAir = false;
                --jumps;
				sfxPlayer.PlaySFX("jump");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        print(jumps);
        rb.velocity = new Vector2(horiVelocity * speed, rb.velocity.y);
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     if (hasntJumped)
        //     {
        //         //rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        //     }

        // }


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }

        print("Jumps: " + jumps + " Hasn't Jumped: " + hasntJumped + " Hasn't Jumped In Air: " + hasntJumpedInAir);

        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        hasntJumped = true;
        isGrounded = true;
    }
}