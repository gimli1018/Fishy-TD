using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Controller : MonoBehaviour
{
    public float spawnTime;
    public float spawnTimeMax = 3;
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
            spawnTime = spawnTimeMax;
        }

    }
}
