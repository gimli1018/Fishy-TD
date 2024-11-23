using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Placement : MonoBehaviour
{
    public Grid grid;
    public GameObject basicTower;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        //Debug.Log(gridPosition);

    }
}
