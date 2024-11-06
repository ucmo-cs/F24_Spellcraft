using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnerScript : MonoBehaviour
{
    public GameObject[] Slimes;
    void Start()
    {
        StartCoroutine("spawnSlime");
    }

    IEnumerator spawnSlime()
    {
        Instantiate(Slimes[Random.Range(0, 3)]);
        yield return new WaitForSeconds(10f);
        StartCoroutine("spawnSlime");
    }
}
