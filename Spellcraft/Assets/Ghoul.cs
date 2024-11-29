using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;



public class Ghoul : MonoBehaviour
{
    Rigidbody2D rb;
    //public GameObject ScoreManager;
    GameObject player;
    public float distance = 0.5f;
    public float moveSpeed = 3f;
    bool frozen, controlled;    // bools for the spells
    Vector3 target;
    Animator anim;
    SpriteRenderer sr;

    void Awake()
    {
        target = transform.position;    // Safety measure so that the Ghoul doesn't try to leave the map
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("moveGhoul");    // Makes sure the Ghoul can move
        //ScoreManager = GameObject.Find("ScoreManager");     // Only for the score tally system
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        // The Ghoul doesn't start immediately affected by a spell
        controlled = false;
        frozen = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Checks so that the Ghoul doesn't go out of bounds (needs to be revised for new play area)
        if (target.x < -7f)
            target.x = -7f;
        else if (target.x > 7.2f)
            target.x = 7.2f;
        if (target.y < -3.8f)
            target.y = -3.8f;
        else if (target.y > 4.5f)
            target.y = 4.5f;
        // Only lets them move if they should be able to
        if (!frozen && !controlled)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        // If the Ghoul isn't moving anymore, they shouldn't look like they're moving
        if (target == transform.position)
        {
            anim.SetBool("Side", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
        }

        // Allows the player to go back to the player
        if (Input.GetKey(KeyCode.Escape) && controlled)
        {
            controlled = false;
            // This is so that the Ghoul doesn't have 20 different instances of movement script telling it to move
            StopAllCoroutines();    // Since we only have one coroutine, this is fine
            StartCoroutine("moveGhoul");
            // This is purely for if the Ghoul was frozen when controlled so that the player can still move again
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerMovement>().changePlayer(player);
            // Without this, the Ghoul will just try to move back to where it was/was going before being controlled
            target = transform.position;
        }
    }

    IEnumerator moveGhoul()
    {
        // Makes it wait first so that it can be frozen and still continue moving
        yield return new WaitForSeconds(3f);
        // Movement is truly random
        switch (Random.Range(0, 3))
        {
            case 0:
                // Left
                anim.SetBool("Side", true);
                sr.flipX = false;
                target = new Vector3(transform.position.x - distance, transform.position.y);
                break;
            case 1:
                // Right
                anim.SetBool("Side", true);
                sr.flipX = true;
                target = new Vector3(transform.position.x + distance, transform.position.y);
                break;
            case 2:
                // Down
                anim.SetBool("Down", true);
                sr.flipX = false;
                target = new Vector3(transform.position.x, transform.position.y - distance);
                break;
            case 3:
                // Up
                anim.SetBool("Up", true);
                sr.flipX = false;
                target = new Vector3(transform.position.x, transform.position.y + distance);
                break;
        }
        // Looping forever
        StartCoroutine("moveGhoul");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Switch case for spells to make it easier on the machine
        switch (collision.gameObject.tag)
        {
            case "Fire":

                 Destroy(collision.gameObject);
                // Since fireball can unfreeze, it has an if-else for that
                if (!frozen)
                {
                    //ScoreManager.GetComponent<ScoreManagerScript>().score += 1;
                    Destroy(this.gameObject);
                }
                else
                {
                    StopAllCoroutines(); // This works as long as no other coroutines are made in this script
                    StartCoroutine("moveGhoul");
                    anim.SetBool("Frozen", false);
                    frozen = false;
                }
                break;
            case "Telekinesis":
                StopAllCoroutines();
                Vector2 mover = collision.gameObject.transform.position;
                Vector2 dist =  Vector2.zero;
                if (transform.position.x < mover.x -.1)
                    dist.x = -1; 
                else if (transform.position.x > mover.x + .1)
                    dist.x = 1;
                if(transform.position.y < mover.y - .1)
                    dist.y = -1;
                else if (transform.position.y > mover.y + .1)
                    dist.y = 1;

                transform.position = new Vector3(transform.position.x + dist.x, transform.position.y + dist.y);
                target = transform.position;
                Destroy(collision.gameObject);
                break;

            case "Remote Control":
                StopAllCoroutines();    // As stated multiple times above, this stops all coroutines, not just moveGhoul;
                // if-else to make sure frozen will be unable to move
                if (!frozen)
                {
                    player.GetComponent<PlayerMovement>().changePlayer(this.gameObject);
                }
                // To make it so that frozen Ghouls can't move, we just don't send control over but we remove player movement
                else
                {
                    GameObject.Find("Trajectory").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Trajectory").GetComponent<SpellsBase>().empty;
                    GameObject.Find("Trajectory").GetComponent<SpellsBase>().enabled = false;
                    player.GetComponent<PlayerMovement>().enabled = false;
                }
                controlled = true;
                Destroy(collision.gameObject);
                break;
            case "Gateway":
                target = collision.transform.position;
                break;
        }
    }
}


