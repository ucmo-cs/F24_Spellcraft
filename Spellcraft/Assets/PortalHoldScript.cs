using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalHoldScript : MonoBehaviour
{
    public GameObject Portal1;
    public GameObject Portal2;
    bool switcher = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
    public void NewPort(GameObject newPortal)
    {
        newPortal.GetComponent<GatewayPortalScript>().Parent = gameObject;
        if (switcher)
        {
            newPortal.GetComponent<GatewayPortalScript>().num = 1;
            Portal1 = newPortal;
        }
        else
        {
            newPortal.GetComponent<GatewayPortalScript>().num = 2;
            Portal2 = newPortal;
        }
        switcher = !switcher;
    }

}