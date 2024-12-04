using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int fireHitsToKill = 5;  // Number of fire hits required to kill the boss
    public int freezeHitsToFreeze = 3; // Number of freeze hits required to freeze the boss
    private int currentFireHits = 0;
    private int currentFreezeHits = 0;

    public GameObject ScoreManager;
    public float distance = 0.5f;
    public float moveSpeed = 3f;
    private bool frozen;
    private Vector3 target;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        target = transform.position; // Prevents the boss from moving outside the map
        StartCoroutine("MoveBoss");
        ScoreManager = GameObject.Find("ScoreManager"); // Only for the score tally system
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        frozen = false;
    }

    void Update()
    {
        if (!frozen)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

        // Stop animations when not moving
        if (target == transform.position)
        {
            anim.SetBool("Side", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
        }
    }

    IEnumerator MoveBoss()
    {
        while (true)
        {
            // Wait before changing direction
            yield return new WaitForSeconds(3f);

            // Random movement
            switch (Random.Range(0, 4))
            {
                case 0: // Left
                    anim.SetBool("Side", true);
                    sr.flipX = false;
                    target = new Vector3(transform.position.x - distance, transform.position.y);
                    break;
                case 1: // Right
                    anim.SetBool("Side", true);
                    sr.flipX = true;
                    target = new Vector3(transform.position.x + distance, transform.position.y);
                    break;
                case 2: // Down
                    anim.SetBool("Down", true);
                    target = new Vector3(transform.position.x, transform.position.y - distance);
                    break;
                case 3: // Up
                    anim.SetBool("Up", true);
                    target = new Vector3(transform.position.x, transform.position.y + distance);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fire":
                int damage = frozen ? 10 : 1;
                currentFireHits += damage;
                Destroy(collision.gameObject);

                if (frozen)
                {
                    StopAllCoroutines();
                    anim.SetBool("Frozen", false);
                    frozen = false;
                    StartCoroutine("MoveBoss");
                }

                if (currentFireHits >= fireHitsToKill)
                {
                    ScoreManager.GetComponent<ScoreManagerScript>().score += 1;
                    Destroy(gameObject);
                }
                break;

            case "Freeze":
                currentFreezeHits++;
                Destroy(collision.gameObject);

                if (currentFreezeHits >= freezeHitsToFreeze)
                {
                    StopAllCoroutines();
                    anim.SetBool("Frozen", true);
                    frozen = true;
                    StartCoroutine(UnfreezeAfterDelay());
                }
                break;

            default:
                break;
        }
    }

    private IEnumerator UnfreezeAfterDelay()
    {
        // Boss stays frozen for 5 seconds before resuming movement
        yield return new WaitForSeconds(5f);
        anim.SetBool("Frozen", false);
        frozen = false;
        StartCoroutine("MoveBoss");
    }
}
