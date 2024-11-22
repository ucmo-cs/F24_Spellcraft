using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GatewayPortalScript : MonoBehaviour
{
    public GameObject Parent;
    public int num=0;
    public int direction=0;
    public BoxCollider2D box;
    public bool open=true;
    public Animator anim;
    public AudioSource aud;
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        direction = (int)transform.localRotation.z / 90;
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (Parent.GetComponent<PortalHoldScript>().Portal2 != null & open)
        {
            aud.Play();
            //Parent.GetComponent<PortalHoldScript>().Portal1.GetComponent<GatewayPortalScript>().open = false;
            //Parent.GetComponent<PortalHoldScript>().Portal1.GetComponent<GatewayPortalScript>().open = false;
            if (num == 1) {
                //collision.transform.position = Parent.GetComponent<PortalHoldScript>().Portal2.transform.position;
            }
            else {
                //collision.transform.position = Parent.GetComponent<PortalHoldScript>().Portal1.transform.position;
            }
        }
        Invoke("opening", 1f);*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        opening();
    }
    void opening()
    {
        open = true;
    }
}
