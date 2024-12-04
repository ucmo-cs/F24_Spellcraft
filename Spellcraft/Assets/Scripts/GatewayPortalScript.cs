using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GatewayPortalScript : MonoBehaviour
{
    GameObject Parent;
    public int num;
    public float rotation;
    public Animator anim;
    public AudioSource aud;
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Parent = GameObject.Find("PortalHolder");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        aud.Play();
        Parent.GetComponent<PortalHoldScript>().Transport(num, collision.transform, collision.gameObject.tag);
    }
}
