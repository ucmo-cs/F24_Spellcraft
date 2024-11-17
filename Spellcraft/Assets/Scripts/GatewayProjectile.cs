using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewayProjectile : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;
    Vector3 hit;
    bool collided;

    void Start()
    {
        // The gameobject always starts with a -90 degree rotation for some reason so this fixes it
        transform.Rotate(0f, 0f, 90f);
        // This makes it so we don't crash them if they play enough
        Destroy(gameObject, 1.5f);
        rb = this.GetComponent<Rigidbody2D>();
        collided = false;
    }

    void Update()
    {
        // Makes the projectile go straight according to the angle it's supposed to go
        rb.velocity = transform.right * velocity;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Wall" && !collided) {
            collided = true;
            hit = col.ClosestPoint(transform.position).normalized;
            Destroy(this.gameObject);
            /*StopAllCoroutines();
            StartCoroutine("gatewayCreation");*/
            Debug.Log(hit);
            float angle = Vector3.Angle(hit, Vector3.up);
            Debug.Log(angle);
            if(Mathf.Approximately(angle, 0)) {
                // Down
                Debug.Log("Down");
            }
            else if(Mathf.Approximately(angle, 180)) {
                // Up
                Debug.Log("Up");
            }
            else if(Mathf.Approximately(angle, 90)) {
                // Sides
                Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                if(cross.y > 0) {
                    // Left Side
                    Debug.Log("Left");
                }
                else {
                    // Right Side
                    Debug.Log("Right");
                }
            }
        }
    }

    /*IEnumerator gatewayCreation()
    {
        yield return new WaitForSeconds(0.5f);
    }*/
}
