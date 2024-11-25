using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Controller : MonoBehaviour
{
    public float spawnTime;
    public float spawnTimeMax = 4;
    public GameObject enemyOne;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = spawnTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Instantiate(enemyOne);

            if(spawnTimeMax > 1)
            {
                spawnTimeMax -= .07f; // Decreases the spawn timer by each time, in a 180s game, that's normally 45 spawns 
            }
            spawnTime = spawnTimeMax;
        }

    }
}
