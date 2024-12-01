using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerScript : MonoBehaviour
{
    public int MaxSlimes = 8;
    private int numSlimes = 0;

    public GameObject Slime;

    private string spawnerSortingLayer;
    void Start()
    {
        Renderer spawnerRenderer = GetComponent<Renderer>();
        if (spawnerRenderer != null)
        {
            spawnerSortingLayer = spawnerRenderer.sortingLayerName;
        }
        else
        {
            Debug.LogWarning("No Renderer found on the SlimeSpawner. Using default sorting layer.");
            spawnerSortingLayer = "Default";
        }

        StartCoroutine("spawnSlime");
    }

    IEnumerator spawnSlime()
    {
        if (numSlimes < MaxSlimes) // Changed to < instead of <= to ensure no overflow
        {
            GameObject newSlime = Instantiate(Slime, transform.position, Quaternion.identity);

            // Set the sorting layer of the new slime
            Renderer slimeRenderer = newSlime.GetComponent<Renderer>();
            if (slimeRenderer != null)
            {
                slimeRenderer.sortingLayerName = spawnerSortingLayer;
            }
            else
            {
                Debug.LogWarning("No Renderer found on the instantiated slime. Sorting layer not set.");
            }

            numSlimes++;
            yield return new WaitForSeconds(5f);
            StartCoroutine("spawnSlime");
        }
    }
}
