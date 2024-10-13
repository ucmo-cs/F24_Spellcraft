using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveScript : MonoBehaviour
{
    public int dir = 4;
    public bool isFrozen = false;
    public float Speed = .1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake()
    {
        sprite=GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Invoke("ticker", 4);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Move(dir);
    }
    void Move(int Direction=0)
    {
        Colorer();
        sprite.flipX = Direction == 1;
        if (!isFrozen)
        {
            anim.SetBool("Frozen", false);
            switch (Direction%5) { 
                case 1:
                    rb.MovePosition(transform.position+transform.right * Speed);
                    anim.SetInteger("Direction", 1);
                    break;
                case 2:
                    rb.MovePosition(transform.position + transform.up * -Speed);
                    anim.SetInteger("Direction", 2);
                    break;
                case 3:
                    rb.MovePosition(transform.position + transform.right * -Speed);
                    anim.SetInteger("Direction", 3);
                    break;
                case 4:
                    anim.SetInteger("Direction", 4);
                    rb.MovePosition(transform.position + transform.up * Speed);
                    break;
                default:
                    anim.SetInteger("Direction", 2);
                    break;
            }
        }
        else
            {
            anim.SetBool("Frozen", true);
            rb.MovePosition(transform.position);
            }
    }
    void ticker()
    {
        if (isFrozen)
        {
            dir =Random.Range(1,4);
        }
       Invoke("timer", 4);

    }
    void Colorer()
    {
        if (Speed <= 0)
        {
            sprite.color= new Color(0.248f,  1,1);
        }
        else if (Speed <= .05)
        {
            sprite.color = new Color(0.6494812f,1, 0.6078432f);
        }
        else {
            sprite.color = new Color(1, 0.6084906f, 0.6084906f);
        }


    }


}