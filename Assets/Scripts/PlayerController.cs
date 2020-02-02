using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   private Rigidbody2D rb;
   private float horiVelocity = 0.0f;  // Horizontal Velocity. Set by player movement.
   public Animator animator;
   public float speed;
   public float jumpHeight;
	public bool hasntJumped;


   // Start is called before the first frame update
   void Start()
   {
      rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
   }

   void Update()
   {
      // sets animator variable
      animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
      horiVelocity = Input.GetAxis("Horizontal");
   }

   void FixedUpdate()
   {
      rb.velocity = new Vector2(horiVelocity * speed, rb.velocity.y);
      if (hasntJumped)
      {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            hasntJumped = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
         }
      }

   }

	void OnCollisionEnter2D(Collision2D other) {
		hasntJumped = true;

	}
}