using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SfxPlayer sfxPlayer;
    private float horiVelocity = 0.0f;  // Horizontal Velocity. Set by player movement.
    public Animator animator;
    GameObject[] pickups;

    public float speed;
    public float jumpHeight;
    private bool hasntJumped;

    private bool hasntJumpedInAir;
    private bool isGrounded;
    private bool isActive;
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
        isActive = true;
        sfxPlayer = GetComponent<SfxPlayer>() as SfxPlayer;
        pickups = GameObject.FindGameObjectsWithTag("Pickup");
    }

    void Update()
    {
        animator.SetFloat("VertSpeed", rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        animator.SetBool("Jumped Once", hasntJumped);
        animator.SetBool("Double Jumped", hasntJumpedInAir);
        animator.SetInteger("Jumps", jumps);
        horiVelocity = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horiVelocity) > 0 && hasntJumped)
        {
            //sfxPlayer.PlaySFX("walk");
        }
        else
        {
            //sfxPlayer.StopWalking();
        }

        //Un-jumping code
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jumps > 0)
            {
                hasntJumpedInAir = true;
            }
        }

        //Jumping code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hasntJumped)
            {
                //Grounded / saved jump
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                hasntJumped = false;
                sfxPlayer.PlaySFX("jump");
            }
            else if (hasntJumpedInAir)
            {
                //Double Jump
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                hasntJumpedInAir = false;
                --jumps;
                sfxPlayer.PlaySFX("jump");
                animator.SetInteger("Jumps", jumps);
            }
            else
            {
                //Used all double jumps
                Explode();
            }
        }
    }



    void FixedUpdate()
    {
        rb.velocity = new Vector2(horiVelocity * speed, rb.velocity.y);

        //Jumping physics code
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }
        //resets grounded state to false, overridden by OnCollisionEnter2D
        isGrounded = false;

        if (!isActive)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        //print("Jumps: " + jumps + " Hasn't Jumped: " + hasntJumped + " Hasn't Jumped In Air: " + hasntJumpedInAir);

    }

    public void AddJump()
    {
        if(jumps < 2)
			jumps = 2;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.GetContact(0).point.y > other.gameObject.transform.position.y + other.gameObject.GetComponent<SpriteRenderer>().size.y - .25F)
        {
            hasntJumped = true;
            isGrounded = true;
            if (pickups.Length > 0) {
                foreach (GameObject p in pickups) {
                    p.SetActive(true);
                    Debug.Log(p);
                }
            }
        }
    }


    void Explode()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    IEnumerator ExplodeCoroutine()
    {
        //broken camera zoom in
        //Camera.main.GetComponent<CameraController>().ZoomIn();

        //broken slow down
        //Time.timeScale = 0.2F;

        yield return new WaitForSecondsRealtime(.25F);

        //Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }


}