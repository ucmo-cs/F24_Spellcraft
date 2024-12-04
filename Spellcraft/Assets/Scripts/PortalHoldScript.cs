using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalHoldScript : MonoBehaviour
{
    public GameObject[] portals;
    public GameObject portalPrefab;
    int index;
    bool open;

    private void Awake()
    {
        portals = new GameObject[2];
        index = 0;
        open = false;
    }

    public void PlacePortal(Vector3 portal)
    {
        if(index > 1) {
            Destroy(portals[index%2]);
        }
        portals[index%2] = Instantiate(portalPrefab, new Vector3(portal.x, portal.y), Quaternion.Euler(0, 0, portal.z));
        portals[index%2].GetComponent<GatewayPortalScript>().num = index % 2;
        index++;
        if(index >= 2) {
            open = true;
        }
    }

    public void Transport(int num, Transform trans, string tag)
    {
        if(open) {
            open = false;
            num = (num + 1) % 2;
            trans.position = portals[num].transform.position;
            switch(tag) {
                case "Fire":
                case "Freeze":
                case "Telekinesis":
                case "Gateway":
                case "Remote Control":
                case "Resize":
                    trans.Rotate(0f, 0f, Mathf.Abs(portals[(num+1)%2].transform.eulerAngles.z - portals[num].transform.eulerAngles.z) + 270f);
                    break;
            }
            Invoke("OpenPortal", 0.1f);
        }
    }

    void OpenPortal()
    {
        open = true;
    }
}