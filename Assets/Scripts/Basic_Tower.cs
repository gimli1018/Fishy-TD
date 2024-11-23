
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Tower : MonoBehaviour
{
    [SerializeField] private bool towerShoot;
    [SerializeField] Animator animatorTowerShoot;
    [SerializeField] private bool towerWall;
    [SerializeField] Animator animatorTowerWall;

    [SerializeField] private float shootTimer;
    [SerializeField] private float shootTimerMax = 3;
    [SerializeField] private GameObject projecticle;

    [SerializeField] private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootTimerMax;

        if (towerWall) // wall towers have 3 times the health of shooters
        {
            health *= 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if(shootTimer <= 0 && towerShoot) // towerShoot to see if this is basic shooting tower, else it'll be a wall tower
        {
            animatorTowerShoot.SetBool("isShooting", true);

            shootTimer = shootTimerMax;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Basic Tower: I have touched " + other.gameObject.name);
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void towerFireProjectile()
    {
        //Debug.Log("Basic Tower: Shoot");
        Instantiate(projecticle, this.transform.position, Quaternion.identity); // the rotation is wrong for the triangle I use
    }


    public void setisShootingFalse()
    {
        animatorTowerShoot.SetBool("isShooting", false);
    }
}
