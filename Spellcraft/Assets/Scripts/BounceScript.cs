using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    // Speed at which the projectile will fly
    float velocity = 10f;
    Rigidbody2D rb;
    public GameObject wallCheck;
    bool both, halfcol;
    Transform pos1, pos2;

    void Start()
    {
        // We turn the object around so that it can move backwards to measure the direction the portal should face
        transform.Rotate(0f, 0f, 180f);
        // This lets us move the object
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine("StopMovement");
        both = false;
        halfcol = false;
    }

    void Update()
    {
        // Makes the projectile go straight according to the angle it's supposed to go
        rb.velocity = transform.right * velocity;
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        velocity = 0f;
        transform.Rotate(0f, 0f, 180f);
        Instantiate(wallCheck, this.transform.position, Quaternion.Euler(new Vector3(0, 0, this.transform.eulerAngles.z - 10f)));
        Instantiate(wallCheck, this.transform.position, Quaternion.Euler(new Vector3(0, 0, this.transform.eulerAngles.z + 10f)));
    }

    public void receiveCheck(Transform pos, bool collide)
    {
        if(pos != pos1 && pos != pos2) {
            if(collide) {
                if(both) {
                    pos2 = pos;
                    wallCalculate();
                }
                else {
                    pos1 = pos;
                    both = true;
                }
            }
            else {
                if(!both) {
                    Debug.Log("Something went wrong. First check failed");
                }
                else {
                    halfcol = true;
                    wallCalculate();
                }
            }
        }
    }

    void wallCalculate()
    {
        if(pos1.rotation.z == this.transform.rotation.z + 10f) {
            Transform temp = pos1;
            pos1 = pos2;
            pos2 = temp;
        }
        float angle = this.transform.eulerAngles.z;
        Debug.Log(angle);
        if(halfcol) {
            if((angle > 0f && angle < 45f) && (angle > 315f && angle < 360f)) {
                Debug.Log("Down-facing");
            }
            else if(angle > 45f && angle < 135f) {
                Debug.Log("Right-facing");
            }
            else if(angle > 135f && angle < 225f) {
                Debug.Log("Up-facing");
            }
            else if(angle > 225f && angle < 315f) {
                Debug.Log("Left-facing");
            }
        }
        float dist1 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(pos1.position.x - this.transform.position.x, 2f) + Mathf.Pow(pos1.position.y - this.transform.position.y, 2f)));
        float dist2 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(pos2.position.x - this.transform.position.x, 2f) + Mathf.Pow(pos2.position.y - this.transform.position.y, 2f)));
        // Down-facing wall is between (0 and 90) and (270 and 360) with it excluding 90 and 270
        // Up-facing wall is between 90 and 270 exclusively
        // Right-facing wall is between 0 and 180 exclusively
        // Left-facing wall is between 180 and 360 exclusively
        if(angle > 0f && angle < 90f) // Contending between down-facing and right-facing
        {
            if(dist1 < dist2) {
                Debug.Log("Down-facing");
            }
            else if(dist1 > dist2) {
                Debug.Log("Right-facing");
            }
        }
        else if(angle > 90f && angle < 180f) // Contending between right-facing and up-facing
        {
            if(dist1 < dist2) {
                Debug.Log("Right-facing");
            }
            else if(dist1 > dist2) {
                Debug.Log("Up-facing");
            }
        }
        else if(angle > 180f && angle < 270f) // Contending between up-facing and left-facing
        {
            if(dist1 < dist2) {
                Debug.Log("Up-facing");
            }
            else if(dist1 > dist2) {
                Debug.Log("Left-facing");
            }
        }
        else if(angle > 270f && angle < 360f) // Contending between left-facing and down-facing
        {
            if(dist1 < dist2) {
                Debug.Log("Left-facing");
            }
            else if(dist1 > dist2) {
                Debug.Log("Down-facing");
            }
        }
    }
}
