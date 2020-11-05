using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    Rigidbody2D rb;
    public float speed = 1;
    public float jumpForce = 3;
    private bool queueJump = false;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        if (x != 0) transform.position = transform.position.AddX(x);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            queueJump = true;
        }
    }

    void FixedUpdate() {
        if (queueJump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            queueJump = false;
        }
    }
}
