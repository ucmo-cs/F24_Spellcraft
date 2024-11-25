using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallCheckScript : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;

    void Start()
    {
        StartCoroutine("DestroySelf");
        rb = this.GetComponent<Rigidbody2D>();
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
            GameObject.Find("WallBouncer(Clone)").GetComponent<BounceScript>().receiveCheck(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.eulerAngles.z), true);
            StopCoroutine("DestroySelf");
            //this.gameObject.SetActive(false);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Timer");
        GameObject.Find("WallBouncer(Clone)").GetComponent<BounceScript>().receiveCheck(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.eulerAngles.z), false);
        //this.gameObject.SetActive(false);
    }
}
