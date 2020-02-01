using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> () as Rigidbody2D;
    }

    void fixedUpdate() {
            float move = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(move*speed, rb.velocity.y);
    }

}