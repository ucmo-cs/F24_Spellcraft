using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalHoldScript : MonoBehaviour
{
    //public GameObject Portal1;
    //public GameObject Portal2;
    bool switcher = false;
    public GameObject[] portals;
    public GameObject portalPrefab;
    GameObject bounce;
    float bounceRotate;
    int index;

    private void Awake()
    {
        portals = new GameObject[2];
        index = 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
    /*public void NewPort(GameObject newPortal)
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
    }*/

    public void PlacePortal(Transform portal, GameObject bounce)
    {
        //portals[index%2] = Instantiate(portalPrefab, portal);
        //index++;
        this.bounce = bounce;
        StartCoroutine("RotateAssist");
        float rot = portal.rotation.z - bounceRotate;
        Debug.Log(rot);
    }

    IEnumerator RotateAssist()
    {
        yield return new WaitForSeconds(0.1f);
        bounceRotate = bounce.transform.rotation.z;
        Destroy(bounce);

    }
}