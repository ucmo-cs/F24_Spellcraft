using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    public GameObject Go;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            enlarge(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            enlarge(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            enlarge(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            enlarge(3);
        }
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            enlarge(4);
        }
        else if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6))
        {
            enlarge(5);
        }
    }
    void enlarge(int input)
    {
        
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            if (i != input)
                Shrink(i);
            else {
                Go = this.gameObject.transform.GetChild(input).gameObject;
                Go.transform.localScale = new Vector3(1.2f, 1.2f, transform.localScale.z);
            }
        }
    }
    void Shrink(int input)
    {
        Go = this.gameObject.transform.GetChild(input).gameObject;
        Go.transform.localScale = new Vector3(1f, 1f, transform.localScale.z);
    }
}
