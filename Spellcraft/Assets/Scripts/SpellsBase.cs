using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class SpellsBase : MonoBehaviour
{
    public Image SpellInfo;
    public GameObject path;
    public GameObject spell;
    bool stopSpell = true;
    bool movement = true;
    bool rotating = false;
    bool queued = false;
    float speed = 20f;
    float rotation = 0f;
    void Start()
    {
        SpellInfo.gameObject.SetActive(false);
        path = GameObject.FindGameObjectWithTag("Trajectory");
        path.gameObject.SetActive(false);
    }

    void Update()
    {
        // Calls the function that controls the trajectory's movement
        prepareSpell();
        // If statement so that the trajectory will show while it's moving but close once it's done
        if(queued && !rotating) {
            hideSpellInfo();
            stopSpell = true;
            queued = false;
        }
        // This will show the trajectory and keybinds if either shift key is held
        else if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !rotating) {
            showSpellInfo();
            stopSpell = false;
        }
        // This will hide the trajectory and keybinds once either shift key is released
        else if((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) {
            if(!rotating) {
                hideSpellInfo();
                stopSpell = true;
            }
            // The if statement is so that it will only hide it once the trajectory stops moving
            else {
                queued = true;
            }
        }
        // If the trajectory needs to rotate, it will call the function to rotate it
        else if(rotating) {
            rotateSpell();
        }
    }

    // Short function to show the trajectory and keybinds
    void showSpellInfo()
    {
        SpellInfo.gameObject.SetActive(true);
        path.gameObject.SetActive(true);
    }

    // Short function to hide the trajectory and keybinds
    void hideSpellInfo()
    {
        SpellInfo.gameObject.SetActive(false);
        path.gameObject.SetActive(false);
    }

    // Not-so-short function to calculate where the trajectory should go based on the keys input
    void prepareSpell()
    {
        // This just checks if we are currently showing the trajectory or not and prevents the player from moving so it doesn't mess up the trajectory
        if(!stopSpell && movement) {
            // Override movement script
            movement = false;
        }
        // If they can't move, than we check if all 8 key combinations
        else if(!movement) {
            // This checks if they are holding down a "Left" key (e.g. LeftArrowKey or A)
            if(Input.GetAxisRaw("Horizontal") < 0) {
                // This checks if they are holding down a "Down" key in addition (e.g. DownArrowKey or S)
                if(Input.GetAxisRaw("Vertical") > 0) {
                    rotation = 45f;
                    rotating = true;
                }
                // This checks if they are holding down an "Up" key in addition (e.g. UpArrowKey or W)
                else if(Input.GetAxisRaw("Vertical") < 0) {
                    rotation = 135f;
                    rotating = true;
                }
                // If they aren't holding an up or down key, then they are only looking to check left
                else {
                    rotation = 90f;
                    rotating = true;
                }
            }
            // Since they aren't holding left, we wanna check if they are holding a "Right" key (e.g. RightArrowKey or D)
            else if(Input.GetAxisRaw("Horizontal") > 0) {
                // Checks for down in addition to right
                if(Input.GetAxisRaw("Vertical") < 0) {
                    rotation = 225f;
                    rotating = true;
                }
                // Checks for up in addition to right
                else if(Input.GetAxisRaw("Vertical") > 0) {
                    rotation = 315f;
                    rotating = true;
                }
                // Since they aren't holding up or down, they only want right
                else {
                    rotation = 270f;
                    rotating = true;
                }
            }
            // Since they aren't holding left or right, we no longer check for combinations and only up
            else if(Input.GetAxisRaw("Vertical") > 0) {
                rotation = 0f;
                rotating = true;
            }
            // Since they aren't holding left, right, or up, we check if they are holding down. Otherwise, they aren't holding any keys we care about
            else if(Input.GetAxisRaw("Vertical") < 0) {
                rotation = 180f;
                rotating = true;
            }
            else if(Input.GetKeyUp(KeyCode.E)) {
                GameObject tempSpell = Instantiate(spell, path.transform.position, path.transform.rotation);
                tempSpell.transform.right = path.transform.right.normalized;
            }
        }
        // Since they aren't being shown the menu anymore, we return movement control back
        else if(stopSpell && !movement) {
            // Return movement script permissions
            movement = true;
        }
    }

    // Function that performs the smooth rotation
    void rotateSpell()
    {
        // This checks if it needs to rotate anymore
        if(path.transform.rotation == Quaternion.Euler(0f, 0f, rotation)) {
            rotating = false;
        }
        // Since it does need to rotate, we perform the rotation
        else {
            path.transform.rotation = Quaternion.Slerp(path.transform.rotation, Quaternion.Euler(0f, 0f, rotation), speed * Time.deltaTime);
        }
    }
}
