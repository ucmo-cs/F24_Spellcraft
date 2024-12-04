using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewayProjectile : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;
    public GameObject bouncePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // The gameobject always starts with a -90 degree rotation for some reason so this fixes it
        transform.Rotate(0f, 0f, 90f);
        // This makes it so we don't crash them if they play enough
        Destroy(gameObject, 1.5f);
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the projectile go straight according to the angle it's supposed to go
        rb.velocity = transform.right * velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall") {
            Instantiate(bouncePrefab, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
