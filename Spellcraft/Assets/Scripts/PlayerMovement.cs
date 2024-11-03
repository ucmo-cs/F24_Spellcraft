using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    public float speed = 5f;
    SpellsBase spellScript;
    GameObject player;
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
            Vector3 temp = player.transform.position; ;
            temp.y += speed * Time.deltaTime;
            player.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving down
        else if(Input.GetAxisRaw("Vertical") < 0) {
            Vector3 temp = player.transform.position; ;
            temp.y -= speed * Time.deltaTime;
            player.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                anim.SetBool("Side", false);
                sr.flipX = false;
            }
        }
        // Animates the character moving right
        if(Input.GetAxisRaw("Horizontal") > 0) {
            Vector3 temp = player.transform.position; ;
            temp.x += speed * Time.deltaTime;
            player.transform.position = temp;
            if(!spellScript.casting) {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                sr.flipX = true;
            }
        }
        // Animates the character moving left
        else if(Input.GetAxisRaw("Horizontal") < 0) {
            Vector3 temp = player.transform.position; ;
            temp.x -= speed * Time.deltaTime;
            player.transform.position = temp;
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

    public void changePlayer(GameObject play)
    {
        if(play == this.gameObject) {
            GameObject.Find("Trajectory").GetComponent<SpellsBase>().enabled = true;
        }
        else {
            GameObject.Find("Trajectory").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Trajectory").GetComponent<SpellsBase>().empty;
            GameObject.Find("Trajectory").GetComponent<SpellsBase>().enabled = false;
        }
        player = play;
        anim = play.GetComponent<Animator>();
        sr = play.GetComponent<SpriteRenderer>();
    }
}