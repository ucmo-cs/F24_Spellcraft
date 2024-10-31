using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerScript : MonoBehaviour
{
    public GameObject Slime;
    void Start()
    {
        StartCoroutine("spawnSlime");
    }

    IEnumerator spawnSlime()
    {
        Instantiate(Slime);
        yield return new WaitForSeconds(10f);
        StartCoroutine("spawnSlime");
    }
}
