using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] terrain_prefabs;
    public List<GameObject> active_terrains;
    private int random;
    private GameplayManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameplayManager>();
        //DOWN
        active_terrains[0] = Instantiate(terrain_prefabs[RandomizePrefab()], new Vector3(0.14f, 0.0f, 0.0f), Quaternion.identity);
        active_terrains[1] = Instantiate(terrain_prefabs[RandomizePrefab()], new Vector3(17.12f, 0.0f, 0.0f), Quaternion.identity);
   
    }

    // Update is called once per frame
    void Update()
    {
        //DOWN
        active_terrains[0].transform.Translate(Vector3.left * manager.speed * Time.deltaTime);
        active_terrains[1].transform.Translate(Vector3.left * manager.speed * Time.deltaTime);

        if (active_terrains[0].transform.position.x < -16.7f)
        {
            Destroy(active_terrains[0]);
            active_terrains[0] = active_terrains[1];
            active_terrains[1] = Instantiate(terrain_prefabs[RandomizePrefab()], new Vector3(17.12f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    private int RandomizePrefab()
    {
       if (Random.Range(0.0f, 1.0f) < manager.variation)
        {
            random = Random.Range(1, terrain_prefabs.Length);
        }
        else
        {
            random = 0;
        }
        return random;
    }
}