using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsBase : MonoBehaviour
{
    public GameObject[] spells; // Array of spell prefabs
    public SpriteRenderer trajectory;
    public Sprite empty;
    public Sprite trajectorySprite;
    public Animator anim;
    public int selectedSpellIndex = -1; // Currently selected spell (-1 means none selected)
    public float spellCooldown = 0.5f; // Cooldown between spells
    public bool casting = false;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        trajectory = this.GetComponent<SpriteRenderer>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleSpellSelection();
        HandleSpellFiring();
    }

    // Handles spell selection via keyboard or UI
    private void HandleSpellSelection()
    {
        // Keyboard input for spell selection
        for (int i = 1; i <= spells.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                SelectSpell(i - 1);
            }
        }
    }

    // Allows spell selection via UI
    public void SelectSpell(int spellIndex)
    {
        if (spellIndex >= 0 && spellIndex < spells.Length)
        {
            selectedSpellIndex = spellIndex;
            Debug.Log($"Spell {spellIndex + 1} selected.");
        }
    }

    // Handles firing the selected spell with left click
    private void HandleSpellFiring()
    {
        if (selectedSpellIndex != -1 && Input.GetMouseButtonDown(0) && !casting)
        {
            StartCoroutine(CastSpell());
        }
    }

    // Displays trajectory and rotates to face the mouse position
    private void PrepareSpell()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        trajectory.sprite = trajectorySprite;
    }

    // Hides the trajectory when casting is disabled
    private void HideSpellInfo()
    {
        trajectory.sprite = empty;
    }

    // Casts the selected spell
    private IEnumerator CastSpell()
    {
        casting = true;
        PrepareSpell();

        // Play appropriate animation based on direction
        float zAngle = transform.eulerAngles.z;
        anim.SetBool("CastUp", zAngle > 45f && zAngle <= 135f);
        anim.SetBool("CastSide", (zAngle > 135f && zAngle <= 225f) || (zAngle > 315f || zAngle <= 45f));
        anim.SetBool("CastDown", zAngle > 225f && zAngle <= 315f);

        // Instantiate the selected spell prefab

        GameObject spell = Instantiate(spells[selectedSpellIndex], transform.position, transform.rotation * Quaternion.Euler(0, 0, -90));

        // Adjust its sorting layer to match the player's
        SpriteRenderer spellRenderer = spell.GetComponent<SpriteRenderer>();
        if (spellRenderer != null)
        {
            spellRenderer.sortingLayerName = playerSprite.sortingLayerName;
            spellRenderer.sortingOrder = playerSprite.sortingOrder;
        }

        yield return new WaitForSeconds(spellCooldown);

        // Reset animations
        anim.SetBool("CastUp", false);
        anim.SetBool("CastSide", false);
        anim.SetBool("CastDown", false);

        HideSpellInfo();
        casting = false;
    }
}
