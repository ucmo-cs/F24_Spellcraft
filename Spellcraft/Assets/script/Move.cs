using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public int dir = 4;
    public float Speed = .1f;
    public float timer_rotate = 1f;

    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake()
    {
        sprite=GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Invoke("ticker", timer_rotate);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Move(dir);
    }
    public void Move(int Direction=0)
    {
        sprite.flipX = Direction == 1;
        {
            switch (Direction%5) { 
                case 1:
                    anim.SetInteger("Direction", dir%4);
                    rb.MovePosition(transform.position+transform.right * Speed);
                    break;
                case 2:
                    rb.MovePosition(transform.position + transform.up * -Speed);
                    anim.SetInteger("Direction", dir % 4);
                    break;
                case 3:
                    rb.MovePosition(transform.position + transform.right * -Speed);
                    anim.SetInteger("Direction", dir % 4);
                    break;
                case 4:
                    anim.SetInteger("Direction", dir % 4);
                    rb.MovePosition(transform.position + transform.up * Speed);
                    break;
                default:
                    anim.SetInteger("Direction", 2);
                    break;
            }
        }
    }
    void ticker() { 
    
       dir = Random.Range(1, 4);
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