using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float velocity = 10f;
    float moveHorizontal, moveVertical;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Animates the character moving up
        if(Input.GetAxisRaw("Vertical") > 0) {
            anim.SetBool("Down", false);
            anim.SetBool("Up", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            sr.flipX = false;
        }
        // Animates the character moving down
        else if(Input.GetAxisRaw("Vertical") < 0) {
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            sr.flipX = false;
        }
        // Animates the character moving right
        else if(Input.GetAxisRaw("Horizontal") > 0) {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
            sr.flipX = true;
        }
        // Animates the character moving left
        else if(Input.GetAxisRaw("Horizontal") < 0) {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
            sr.flipX = false;
        }
        // Cancels movement animations so that we don't have movement when we aren't moving
        else {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        }
    }
}
