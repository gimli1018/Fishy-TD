using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] Animator animatorSurfer;
    [SerializeField] GameObject surferSelfDest;

    public int[] startY;
    public bool stopMovement = false;
    [SerializeField] private float speed = 30f;
    
    private bool bounceBack = false;
    [SerializeField] private float bounceTimer;
    [SerializeField] private float bounceTimerMax = 1f;

    [SerializeField] private int health = 6;

    // Start is called before the first frame update
    void Start()
    {
        bounceTimer = bounceTimerMax;

        startY = new int[5];
        startY[0] = -128; // Lane 1
        startY[1] = -64; // Lane 2
        startY[2] = 0; // Lane 3
        startY[3] = 64; // Lane 4
        startY[4] = 128; // Lane 5

        int ranY = Random.Range(0, startY.Length);
        //Debug.Log(ranY);
        this.transform.position = new Vector3(352, startY[ranY], 0);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = new Vector3(1, 0, 0) * speed * Time.deltaTime;

        if (stopMovement == false && !bounceBack)
        {
            //this.transform.position -= new Vector3(speed, 0, 0);
            this.transform.position -= displacement;
        }
        else
        {
            //this.transform.position += new Vector3(speed, 0, 0);
            this.transform.position += displacement;

            bounceTimer -= Time.deltaTime;

            if (bounceTimer <= 0)
            {
                bounceTimer = bounceTimerMax;
                bounceBack = false;
            }
        }

        if (this.transform.position.x <= -320)
        {
            //Debug.Log("You have lost the game");
            SceneManager.LoadScene(2);

            //Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            //stopMovement = true; // should make enemy stop moving forward

            //Debug.Log("Basic Enemy: I have touched " + other.gameObject.name);

            bounceBack = true;

            other.collider.GetComponent<Basic_Tower>().takeDamage(1);
        }
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;

        if (health > 0)
        {
            int rand = Random.Range(8, 15);
            SoundManager.Instance.PlaySFX(rand);
            animatorSurfer.SetTrigger("takeDamage");
            bounceTimer = bounceTimerMax / 3;
            bounceBack = true;
        }
        if (health <= 0)
        {
            int rand = Random.Range(1, 3);
            SoundManager.Instance.PlaySFX(rand);
            Instantiate(surferSelfDest, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
