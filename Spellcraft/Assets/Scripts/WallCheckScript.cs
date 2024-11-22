using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallCheckScript : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;
    bool collided;

    void Start()
    {
        StartCoroutine("DestroySelf");
        rb = this.GetComponent<Rigidbody2D>();
        collided = false;
    }

    void Update()
    {
        // Makes the checker go
        rb.velocity = transform.right * velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall") {
            Debug.Log("Collision");
            GameObject.Find("WallBouncer(Clone)").GetComponent<BounceScript>().receiveCheck(this.transform, true);
            StopCoroutine("DestroySelf");
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Timer");
        GameObject.Find("WallBouncer(Clone)").GetComponent<BounceScript>().receiveCheck(this.transform, false);
        this.gameObject.SetActive(false);
    }
}
