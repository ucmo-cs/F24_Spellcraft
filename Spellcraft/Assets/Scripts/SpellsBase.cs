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
    public Sprite trajectory;
    public Sprite empty;
    void Start()
    {
        SpellInfo.gameObject.SetActive(false);
        path.GetComponent<SpriteRenderer>().sprite = empty;
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
        // Spawns a base spell projectile when the 'E' key is pressed
        if(Input.GetKeyUp(KeyCode.E)) {
            GameObject tempSpell = Instantiate(spell, path.transform.position, path.transform.rotation);
            tempSpell.transform.right = path.transform.right.normalized;
        }
    }

    // Short function to show the trajectory and keybinds
    void showSpellInfo()
    {
        SpellInfo.gameObject.SetActive(true);
        path.GetComponent<SpriteRenderer>().sprite = trajectory;
    }

    // Short function to hide the trajectory and keybinds
    void hideSpellInfo()
    {
        SpellInfo.gameObject.SetActive(false);
        path.GetComponent<SpriteRenderer>().sprite = empty;
    }

    // short function to calculate where the trajectory should go based on the mouse pointer's position
    void prepareSpell()
    {
        // Takes in the mouse's current position
        Vector3 mousePosition = Input.mousePosition;
        // Translates the mouse's current position into something applicable to the game's window
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Sets x and y of the mouse's position to the distance between it and the trajectory's current position
        mousePosition.x = mousePosition.x - path.transform.position.x;
        mousePosition.y = mousePosition.y - path.transform.position.y;
        // Calculates the angle of the where the mouse is in relation to where the trajectory line is (minus 90 degrees for some reason)
        float angle = (Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg) - 90f;
        // Sets the trajectory line's rotation to be on the mouse's position and since we can't snap the mouse, it will look smooth
        path.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
