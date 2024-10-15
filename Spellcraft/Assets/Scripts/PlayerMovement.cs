using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float velocity = 10f;
    public float speed = 5f;
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
            Vector3 temp = this.transform.position; ;
            temp.y += speed * Time.deltaTime;
            this.transform.position = temp;
            anim.SetBool("Down", false);
            anim.SetBool("Up", true);
            anim.SetBool("Side", false);
            sr.flipX = false;
        }
        // Animates the character moving down
        if(Input.GetAxisRaw("Vertical") < 0) {
            Vector3 temp = this.transform.position; ;
            temp.y -= speed * Time.deltaTime;
            this.transform.position = temp;
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
            anim.SetBool("Side", false);
            sr.flipX = false;
        }
        // Animates the character moving right
        if(Input.GetAxisRaw("Horizontal") > 0) {
            Vector3 temp = this.transform.position; ;
            temp.x += speed * Time.deltaTime;
            this.transform.position = temp;
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", true);
            sr.flipX = true;
        }
        // Animates the character moving left
        if(Input.GetAxisRaw("Horizontal") < 0) {
            Vector3 temp = this.transform.position; ;
            temp.x -= speed * Time.deltaTime;
            this.transform.position = temp;
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", true);
            sr.flipX = false;
        }
        // Cancels movement animations so that we don't have movement when we aren't moving
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", false);
        }
    }
}
