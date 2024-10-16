using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public bool isFrozen = false;
    public MoveScript moveScript;
    // Start is called before the first frame update
    void Awake()
    {
        moveScript = GetComponent<MoveScript>();
        Invoke("ticker", 4);
        
    }

    // Update is called once per frame
    void Update()
    {

        moveScript.anim.SetBool("Frozen", isFrozen);
        if (isFrozen)
        {
            moveScript.dir=0;
        }

    }
    void ticker()
    {
        if (isFrozen)
        {
            moveScript.dir =Random.Range(1,4);
        }
       Invoke("timer", 4);

    }


    }
