using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerScript : MonoBehaviour
{
    public int MaxSlimes = 8;
    private int numSlimes = 0;

    public GameObject Slime;
    void Start()
    {
        StartCoroutine("spawnSlime");
    }

    IEnumerator spawnSlime()
    {
        if (numSlimes <= MaxSlimes)
        {
            Instantiate(Slime);
            yield return new WaitForSeconds(5f);
            StartCoroutine("spawnSlime");
            numSlimes++;
        }
    }
}
