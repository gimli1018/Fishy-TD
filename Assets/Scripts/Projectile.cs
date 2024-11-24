using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position += new Vector3(speed, 0, 0);

        if (this.transform.position.x >= 320)
        {
            Destroy(this.gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Projectice: I have touched " + other.gameObject.name);

            other.collider.GetComponent<Enemy_Controller>().takeDamage(1);

            Destroy(this.gameObject);
        }

    }
}
