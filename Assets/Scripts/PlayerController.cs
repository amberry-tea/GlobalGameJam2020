using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector3 move;
    private Vector3 gravity;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();

        move = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown ("d")){
            move.x = -20;
        }
        if(!Input.anyKeyDown){
            move -= move / 3;
        }
        rb.AddForce(move);
    }
    /*
    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.CompareTag("Collider")) {
            gravity = Vector3.zero;
            print(asdwasda);
        }
    }*/
    void OnCollisionEnter(Collision other)
    {
         if (other.gameObject.CompareTag("Collider")) {
            gravity = Vector3.zero;
            print("asdwasda");
        }
    }
}