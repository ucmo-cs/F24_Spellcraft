using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float speed = 5f;
    float moveHorizontal, moveVertical;
    SpellsBase spellScript;
    void Start()
    {
        spellScript = transform.Find("Trajectory").GetComponent<SpellsBase>();
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
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving down
        else if(Input.GetAxisRaw("Vertical") < 0) {
            Vector3 temp = this.transform.position; ;
            temp.y -= speed * Time.deltaTime;
            this.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving right
        if(Input.GetAxisRaw("Horizontal") > 0) {
            Vector3 temp = this.transform.position; ;
            temp.x += speed * Time.deltaTime;
            this.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                sr.flipX = true;
            }
        }
        // Animates the character moving left
        else if(Input.GetAxisRaw("Horizontal") < 0) {
            Vector3 temp = this.transform.position; ;
            temp.x -= speed * Time.deltaTime;
            this.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                sr.flipX = false;
            }
        }
        // Cancels movement animations so that we don't have movement when we aren't moving
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Side", false);
        }
    }
}