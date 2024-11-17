using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;

    void Start()
    {
        // The gameobject always starts with a -90 degree rotation for some reason so this fixes it
        transform.Rotate(0f, 0f, 90f);
        // This makes it so we don't crash them if they play enough
        Destroy(gameObject, 1.5f);
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Makes the projectile go straight according to the angle it's supposed to go
        rb.velocity = transform.right * velocity;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Wall") {
            Destroy(this.gameObject);
        }
    }
}
