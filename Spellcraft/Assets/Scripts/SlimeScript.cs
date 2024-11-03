using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Rendering;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject ScoreManager;
    GameObject player;
    public float distance = 0.5f;
    public float moveSpeed = 3f;
    bool frozen, controlled;
    Vector3 target;
    Animator anim;
    SpriteRenderer sr;
    void Awake()
    {
        target = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("moveSlime");
        ScoreManager = GameObject.Find("ScoreManager");
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        controlled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(target.x < -7f)
            target.x = -7f;
        else if(target.x > 7.2f)
            target.x = 7.2f;
        if(target.y < -3.8f)
            target.y = -3.8f;
        else if(target.y > 4.5f)
            target.y = 4.5f;
        if(!frozen && !controlled) {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        if(target == transform.position) {
            anim.SetBool("Side", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
        }

        if(Input.GetKey(KeyCode.Escape) && controlled) {
            controlled = false;
            StopCoroutine("moveSlime");
            StartCoroutine("moveSlime");
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerMovement>().changePlayer(player);
            target = transform.position;
        }
    }

    IEnumerator moveSlime()
    {
        yield return new WaitForSeconds(3f);
        switch(Random.Range(0, 3)) {
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
        StartCoroutine("moveSlime");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag) {
            case "Fire":
                if(!frozen) {
                    ScoreManager.GetComponent<ScoreManagerScript>().score += 1;
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                }
                else {
                    StopCoroutine("moveSlime");
                    StartCoroutine("moveSlime");
                    anim.SetBool("Frozen", false);
                    frozen = false;
                    Destroy(collision.gameObject);
                }
                break;
            case "Freeze":
                StopCoroutine("moveSlime");
                anim.SetBool("Frozen", true);
                frozen = true;
                Destroy(collision.gameObject);
                break;
            case "Remote Control":
                StopCoroutine("moveSlime");
                Destroy(collision.gameObject);
                if(!frozen) {
                    player.GetComponent<PlayerMovement>().changePlayer(this.gameObject);
                }
                else {
                    GameObject.Find("Trajectory").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Trajectory").GetComponent<SpellsBase>().empty;
                    GameObject.Find("Trajectory").GetComponent<SpellsBase>().enabled = false;
                    player.GetComponent<PlayerMovement>().enabled = false;
                }
                controlled = true;
                break;
        }
    }
}
