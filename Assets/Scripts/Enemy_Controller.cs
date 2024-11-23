using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public int[] startY;
    public bool stopMovement = false;
    [SerializeField] private float speed = .25f;

    [SerializeField] private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        startY = new int[5];
        startY[0] = -128; // Lane 1
        startY[1] = -64; // Lane 2
        startY[2] = 0; // Lane 3
        startY[3] = 64; // Lane 4
        startY[4] = 128; // Lane 5

        int ranY = Random.Range(0, startY.Length);
        //Debug.Log(ranY);
        this.transform.position = new Vector3(-352, startY[ranY], 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovement == false)
        {
            this.transform.position += new Vector3(speed, 0, 0);   
        }

        if (this.transform.position.x >= 320)
        {
            //Debug.Log("You have lost the game");
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Basic Enemy: I have touched " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            stopMovement = true; // should make enemy stop moving forward
        }
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
