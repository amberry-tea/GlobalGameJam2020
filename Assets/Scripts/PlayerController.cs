using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
    private Rigidbody2D rb;
	private float horiVelocity = 0.0f;	// Horizontal Velocity. Set by player movement.
	private bool isActive = true;		// Whether or not the player can control JAM.
    public Animator animator;
	[Tooltip("JAM's movement speed.")]
    public float speed;
	[Tooltip("JAM's jump height.")]
	public float jumpHeight;
	[Tooltip("JAM's gravity.")]
	public float gravity = 9.8f;
	

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> () as Rigidbody2D;
    }

    void Update() {
		// sets animator variable
	    animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

		if(isActive){
				
			horiVelocity = Input.GetAxis("Horizontal");
            
			rb.velocity = new Vector2(horiVelocity*speed, rb.velocity.y);
		}
    }
}