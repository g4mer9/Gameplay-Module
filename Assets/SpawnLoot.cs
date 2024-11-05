using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnLoot : MonoBehaviour
{
    public GameObject healthPack;
    public GameObject ammoPack;

    public void SpawnRandomItem()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Vector3 spawn_pos = new Vector3(transform.position.x, -0.812f, transform.position.z);
            Instantiate(healthPack, spawn_pos, Quaternion.identity);
        }
        else
        {
            Vector3 spawn_pos = new Vector3(transform.position.x, -0.812f, transform.position.z);
            Instantiate(ammoPack, spawn_pos, Quaternion.identity);
        }
    }
}
