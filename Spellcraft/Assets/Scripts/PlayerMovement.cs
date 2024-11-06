using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    public float speed = 5f;
    SpellsBase spellScript; // Allows to disable the casting system when remote controlling
    GameObject player;  // Allows for easier time to switch for remote control
    void Awake()
    {
        spellScript = transform.Find("Trajectory").GetComponent<SpellsBase>();
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        player = this.gameObject;
    }

    void Update()
    {
        // Animates the character moving up
        if(Input.GetAxisRaw("Vertical") > 0) {
            // Movement
            Vector3 temp = player.transform.position; ;
            temp.y += speed * Time.deltaTime;
            player.transform.position = temp;
            // Animation
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving down
        else if(Input.GetAxisRaw("Vertical") < 0) {
            // Movement
            Vector3 temp = player.transform.position; ;
            temp.y -= speed * Time.deltaTime;
            player.transform.position = temp;
            // Animation
            if(!spellScript.casting) {
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving right
        if(Input.GetAxisRaw("Horizontal") > 0) {
            // Movement
            Vector3 temp = player.transform.position; ;
            temp.x += speed * Time.deltaTime;
            player.transform.position = temp;
            // Animation
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                sr.flipX = true;
            }
        }
        // Animates the character moving left
        else if(Input.GetAxisRaw("Horizontal") < 0) {
            // Movement
            Vector3 temp = player.transform.position; ;
            temp.x -= speed * Time.deltaTime;
            player.transform.position = temp;
            // Animation
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

    // Function to allow us to more easily swap control between the player and the enemy
    public void changePlayer(GameObject play)
    {
        // Checks if we're switching to the player character
        if(play == this.gameObject) {
            // Since we're switching back, we want to cast spells
            spellScript.enabled = true;
        }
        // Since it's not the player, we don't want to cast spells
        else {
            // Makes the trajectory line invisible so that it looks like it's disabled
            GameObject.Find("Trajectory").GetComponent<SpriteRenderer>().sprite = spellScript.empty;
            spellScript.enabled = false;
        }
        // Sets up all the parts so that we don't need to change much
        player = play;
        anim = play.GetComponent<Animator>();
        sr = play.GetComponent<SpriteRenderer>();
    }
}