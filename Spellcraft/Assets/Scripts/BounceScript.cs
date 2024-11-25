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
    Vector3 pos1, pos2, start;
    PortalHoldScript portalHold;

    void Start()
    {
        // We turn the object around so that it can move backwards to measure the direction the portal should face
        transform.Rotate(0f, 0f, 180f);
        // This lets us move the object
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine("StopMovement");     // Sets up the calculation so that it can measure 2 distances to tell what kind of wall it is

        // Bools to help shorten and simplify functions
        both = false;
        halfcol = false;

        // Destroys this object so the wall checks don't get confused and never interact with their bounce script
        Destroy(this.gameObject, 1f);

        // Stores the starting position so we can make a portal when all this is done
        start = this.transform.position;

        portalHold = GameObject.Find("PortalHolder").GetComponent<PortalHoldScript>();
    }

    void Update()
    {
        // Makes the projectile go straight according to the angle it's supposed to go
        rb.velocity = transform.right * velocity;
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        velocity = 0f;      // Stops all movement
        transform.Rotate(0f, 0f, 180f);     // Rotates it back around for my own sanity

        // Creation of the wall checkers
        Instantiate(wallCheck, this.transform.position, Quaternion.Euler(new Vector3(0, 0, this.transform.eulerAngles.z - 10f)));
        Instantiate(wallCheck, this.transform.position, Quaternion.Euler(new Vector3(0, 0, this.transform.eulerAngles.z + 10f)));
    }

    public void receiveCheck(Vector3 pos, bool collide)
    {
        // This if is so that we can't take in duplicate collision data from a single wall checker
        if((pos.x != pos1.x && pos.y != pos1.y) && (pos.x != pos2.x && pos.y != pos2.y)) {
            // Checks if it was from a collision or the wall check expiring
            if(collide) {
                // Super simple to check if this is the first or second wall check's data
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
                // This is just a log because if the first data we get is timed out, that means both timed out, which means we hit a dot in space and not a line
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
        // This swaps the first wall check with the second one if the first one is 10 degrees counter-clockwise from the bouncer. This makes me feel more sane
        if(pos1.z == this.transform.eulerAngles.z + 10f) {
            Vector3 temp = pos1;
            pos1 = pos2;
            pos2 = temp;
        }
        // This stores the angle to make this code about 200+ characters shorter
        float angle = this.transform.eulerAngles.z;
        // Testing purposes
        Debug.Log(angle);

        // This checks if we only had 1 collision so that we can massively shorten the code
        if(halfcol) {
            if((angle > 0f && angle < 45f) && (angle > 315f && angle < 360f)) {
                //Debug.Log("Down-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 270f));
            }
            else if(angle > 45f && angle < 135f) {
                //Debug.Log("Right-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 0f));
            }
            else if(angle > 135f && angle < 225f) {
                //Debug.Log("Up-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 90f));
            }
            else if(angle > 225f && angle < 315f) {
                //Debug.Log("Left-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 180f));
            }
        }

        // Since there were 2 collisions, we need to calculate distance between the bouncer and the point of collision
        float dist1 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(pos1.x - this.transform.position.x, 2f) + Mathf.Pow(pos1.y - this.transform.position.y, 2f)));
        float dist2 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(pos2.x - this.transform.position.x, 2f) + Mathf.Pow(pos2.y - this.transform.position.y, 2f)));

        // Cheat sheet for the different wall orientations

        // Down-facing wall is between 0 and 180 exclusively
        // Up-facing wall is between 180 and 360 exclusively
        // Right-facing wall is between 90 and 270 exclusively
        // Left-facing wall is between (0 and 90) and (270 and 360), excluding 0 and 270
        if(angle > 0f && angle < 90f) // Contending between down-facing and left-facing
        {
            if(dist1 < dist2) {
                //Debug.Log("Left-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 180f));
            }
            else if(dist1 > dist2) {
                //Debug.Log("Down-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 270f));
            }
        }
        else if(angle > 90f && angle < 180f) // Contending between right-facing and down-facing
        {
            if(dist1 < dist2) {
                //Debug.Log("Down-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 270f));
            }
            else if(dist1 > dist2) {
                //Debug.Log("Right-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 0f));
            }
        }
        else if(angle > 180f && angle < 270f) // Contending between up-facing and right-facing
        {
            if(dist1 < dist2) {
                //Debug.Log("Right-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 0f));
            }
            else if(dist1 > dist2) {
                //Debug.Log("Up-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 90f));
            }
        }
        else if(angle > 270f && angle < 360f) // Contending between left-facing and up-facing
        {
            if(dist1 < dist2) {
                //Debug.Log("Up-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 90f));
            }
            else if(dist1 > dist2) {
                //Debug.Log("Left-facing");
                portalHold.PlacePortal(new Vector3(start.x, start.y, 180f));
            }
        }
        // Removes wall checker clutter for peace of mind
        GameObject[] checkers = GameObject.FindGameObjectsWithTag("WallCheck");
        foreach(GameObject check in checkers) {
            Destroy(check);
        }
    }
}
