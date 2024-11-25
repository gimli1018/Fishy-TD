
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Tower : MonoBehaviour
{
    [SerializeField] private bool towerShoot;
    [SerializeField] Animator animatorTowerShoot;
    [SerializeField] GameObject towerShootSelfDest;
    [SerializeField] private bool towerWall;
    [SerializeField] Animator animatorTowerWall;
    [SerializeField] GameObject towerWallSelfDest;

    [SerializeField] private float shootTimer;
    [SerializeField] private float shootTimerMax = 3;
    [SerializeField] private GameObject projecticle;

    [SerializeField] private int health = 5;

    [SerializeField] private Grid grid;
    private int gameObjectIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootTimerMax;

        if (towerWall) // wall towers have 3 times the health of shooters
        {
            health *= 3;
        }

        grid = FindObjectOfType<Grid>();
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

    /*void OnCollisionEnter2D(Collision2D other)
    {
        // Is this method not needed?

        Debug.Log("Basic Tower: I have touched " + other.gameObject.name);
    }*/

    public void takeDamage(int damage)
    {
        health -= damage;

        if (towerWall && health > 0)
        {
            int rand = Random.Range(8, 15);
            SoundManager.Instance.PlaySFX(rand);
            animatorTowerWall.SetTrigger("takeDamage");
        }
        if (towerShoot && health > 0)
        {
            int rand = Random.Range(8, 15);
            SoundManager.Instance.PlaySFX(rand);
            animatorTowerShoot.SetTrigger("takeDamage");
        }

        if (health <= 0)
        {
            int rand = Random.Range(1, 3);
            SoundManager.Instance.PlaySFX(rand);
            // This should be for the death anims but wont work right because the destroy will kill it too quick
            if (towerWall)
            {
                //animatorTowerWall.SetTrigger("death");
                Instantiate(towerWallSelfDest, this.transform.position, Quaternion.identity);
            }
            if (towerShoot)
            {
                //animatorTowerShoot.SetTrigger("death");
                Instantiate(towerShootSelfDest, this.transform.position, Quaternion.identity);
            }
            
            //GridData selectedData = GameObject.Find("PlacementSystem").towerData;
            GridData selectedData = PlacementSystem.towerData;
            Vector3Int gridPosition = grid.WorldToCell(this.transform.position - new Vector3(-32, -32, 0));
            Debug.Log(gridPosition + " " + gameObjectIndex);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            selectedData.RemoveObjectAt(gridPosition);

            Destroy(this.gameObject);
        }
    }

    public void towerFireProjectile()
    {
        int rand = Random.Range(3, 7);
        SoundManager.Instance.PlaySFX(rand);
        //Debug.Log("Basic Tower: Shoot");
        Instantiate(projecticle, this.transform.position, Quaternion.identity); // the rotation is wrong for the triangle I use
    }


    public void setisShootingFalse()
    {
        animatorTowerShoot.SetBool("isShooting", false);
    }
}
