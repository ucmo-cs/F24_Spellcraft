using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    GameObject portalManager;
    Vector3 gateway;

    private void Awake()
    {
        portalManager = GameObject.Find("PortalManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gateway") {
            gateway = collision.transform.position;
            Destroy(collision.gameObject);
            Collider[] intersection1 = Physics.OverlapSphere(new Vector3(gateway.x + 0.01f, gateway.y, gateway.z), 0.1f);
            Collider[] intersection2 = Physics.OverlapSphere(new Vector3(gateway.x - 0.01f, gateway.y, gateway.z), 0.1f);
            if(intersection1.Length > 0 || intersection2.Length > 0) {
                Debug.Log("Down-facing Portal");
            }
            else {
                intersection1 = Physics.OverlapSphere(new Vector3(gateway.x + 0.01f, gateway.y, gateway.z), 0.1f);
                intersection2 = Physics.OverlapSphere(new Vector3(gateway.x - 0.01f, gateway.y, gateway.z), 0.1f);
                if(intersection1.Length > 0 || intersection2.Length > 0) {
                    Debug.Log("Up-facing Portal");
                }
                else {
                    intersection1 = Physics.OverlapSphere(new Vector3(gateway.x, gateway.y + 0.01f, gateway.z), 0.1f);
                    intersection2 = Physics.OverlapSphere(new Vector3(gateway.x, gateway.y - 0.01f, gateway.z), 0.1f);
                    if(intersection1.Length > 0 || intersection2.Length > 0) {
                        Debug.Log("Left-facing Portal");
                    }
                    else {
                        intersection1 = Physics.OverlapSphere(new Vector3(gateway.x, gateway.y + 0.01f, gateway.z), 0.1f);
                        intersection2 = Physics.OverlapSphere(new Vector3(gateway.x, gateway.y - 0.01f, gateway.z), 0.1f);
                        if(intersection1.Length > 0 || intersection2.Length > 0) {
                            Debug.Log("Right-facing Portal");
                        }
                    }
                }
            }
        }
    }
}
