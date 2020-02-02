using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolScript : MonoBehaviour
{
    private GameObject player;
    private bool facingRight;
    private float initialXScale;
    private float initialYScale;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player");
        initialXScale = this.transform.localScale.x;
        initialYScale = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.x < this.transform.position.x) {
            facingRight = false;
        } else {
            facingRight = true;
        }

        if (!facingRight) {
            this.transform.localScale = new Vector3(-initialXScale, initialYScale, 1);
        } else {
            this.transform.localScale = new Vector3(initialXScale, initialYScale, 1);
        }
    }
}
