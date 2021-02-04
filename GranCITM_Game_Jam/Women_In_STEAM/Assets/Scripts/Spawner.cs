using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public GameObject spawn_prefab;
    public float spawn_rate;
    public float spawn_delay;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawn_delay, spawn_rate);
    }

  

    void Spawn()
    {
        spawn = Instantiate(spawn_prefab, transform.position, Quaternion.identity);
    }
}
