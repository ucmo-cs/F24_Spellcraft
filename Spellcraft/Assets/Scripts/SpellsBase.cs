using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SpellsBase : MonoBehaviour
{
    //public Image SpellInfo;
    public GameObject[] spells;
    Animator anim;
    SpriteRenderer player;

    public Sprite trajectory;
    public Sprite empty;
    public bool casting;
    int spellIndex;
    void Start()
    {
        //SpellInfo.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = empty;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calls the function that controls the trajectory's movement
        prepareSpell();
        // This will show the trajectory and keybinds if either shift key is held
        if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) {
            showSpellInfo();
        }
        // This will hide the trajectory and keybinds once either shift key is released
        else if((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) {
            hideSpellInfo();
        }
        // Spawns a spell projectile when the appropriate number key is pressed
        if(!casting) {
            if(Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)) {
                spellIndex = 0;
                StartCoroutine("castSpell");
            }
            else if(Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)) {
                spellIndex = 1;
                StartCoroutine("castSpell");
            }
            else if(Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)) {
                spellIndex = 2;
                StartCoroutine("castSpell");
            }
            else if(Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4)) {
                spellIndex = 3;
                StartCoroutine("castSpell");
            }
            else if(Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)) {
                spellIndex = 4;
                StartCoroutine("castSpell");
            }
            else if(Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6)) {
                spellIndex = 5;
                StartCoroutine("castSpell");
            }
        }
    }

    // Short function to show the trajectory and keybinds
    void showSpellInfo()
    {
        //SpellInfo.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = trajectory;
    }

    // Short function to hide the trajectory and keybinds
    void hideSpellInfo()
    {
        //SpellInfo.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = empty;
    }

    // short function to calculate where the trajectory should go based on the mouse pointer's position
    void prepareSpell()
    {
        // Takes in the mouse's current position
        Vector3 mousePosition = Input.mousePosition;
        // Translates the mouse's current position into something applicable to the game's window
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Sets x and y of the mouse's position to the distance between it and the trajectory's current position
        mousePosition.x = mousePosition.x - transform.position.x;
        mousePosition.y = mousePosition.y - transform.position.y;
        // Calculates the angle of the where the mouse is in relation to where the trajectory line is (minus 90 degrees for some reason)
        float angle = (Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg) - 90f;
        // Sets the trajectory line's rotation to be on the mouse's position and since we can't snap the mouse, it will look smooth
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    IEnumerator castSpell()
    {
        // Creating the spell projectile and making sure it's facing the correct way
        GameObject tempSpell = Instantiate(spells[spellIndex], transform.position, transform.rotation);
        tempSpell.transform.right = transform.right.normalized;

        SpriteRenderer spellRenderer = tempSpell.GetComponent<SpriteRenderer>();
        if (spellRenderer != null)
        {
            spellRenderer.sortingLayerID = player.sortingLayerID;
            spellRenderer.sortingOrder = player.sortingOrder; // Matches the player's sorting order as well
        }

        // Calculates the angle you're aiming at so it can play the appropriate casting animation
        float angle = transform.rotation.eulerAngles.z;

        // Checks where they are aiming for the appropriate animation
        if((angle > 0 && angle < 45) || (angle < 360 && angle > 315)) {
            anim.SetBool("CastUp", true);
            player.flipX = false;
        }
        else if((angle >= 45 && angle <= 135)) {
            anim.SetBool("CastSide", true);
            player.flipX = false;
        }
        else if((angle > 135 && angle < 225)) {
            anim.SetBool("CastDown", true);
            player.flipX = false;
        }
        else if((angle > 225 && angle < 315)) {
            anim.SetBool("CastSide", true);
            player.flipX = true;
        }

        // casting bool variable so that they can't rapid fire casts so animations don't break
        casting = true;
        yield return new WaitForSeconds(0.5f);
        casting = false;

        // Resets the animation bools and player flip state
        anim.SetBool("CastSide", false);
        anim.SetBool("CastUp", false);
        anim.SetBool("CastDown", false);
        player.flipX = false;
    }
}
