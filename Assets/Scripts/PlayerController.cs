using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
    private CharacterController controller;
	private Vector2 moveDirection = new Vector2(0,0);
	private bool isGrounded = false;
	private RaycastHit2D ray;
	
	[Tooltip("JAM's animator.")]
	public Animator animator;
	[Tooltip("JAM's movement speed.")]
    public float speed = 5.0f;
	[Tooltip("JAM's jump height.")]
	public float jumpHeight = 5.0f;
	[Tooltip("JAM's gravity.")]
	public float gravity = 9.8f;
	[Tooltip("JAM's maximum fall speed.")]
	public float gravityCap = -255.0f;
	[Tooltip("/!\\DO NOT EDIT IN INSPECTOR/!\\")]
	public bool isActive = true;		// Whether or not the player can control JAM.
	

    // Start is called before the first frame update.
    void Start () {
		
		if(controller == null)
			controller = GetComponent<CharacterController>();
    }
	
	// Frame update.
    void Update() {
		
		animator.SetFloat("Speed", Mathf.Abs(Input.GetInputRaw("Horizontal")));
		
		ray = Physics2D.Raycast(transform.position, Vector2.down);
		isGrounded = (ray.collider != null && ray.distance <= 0.75f);
		
		if(isGrounded){
			
			// If the player presses jump while JAM is grounded,
			// and player control is active, jump.
			if(Input.GetButtonDown("Jump") && isActive){
				moveDirection.y = jumpHeight;
			} else {
				moveDirection.y = 0.0f;
			}
			
		} else {
			
			// If JAM isn't grounded, start falling.
			if(moveDirection.y > gravityCap){
				moveDirection.y -= gravity * Time.deltaTime;
			} else {
				moveDirection.y = gravityCap;
			}
		}
		
		if(isActive){	
			moveDirection.x = Input.GetAxis("Horizontal");
		} else {
			moveDirection.x = 0.0f;
		}
		
		controller.Move(moveDirection * speed * Time.deltaTime);
	}
}