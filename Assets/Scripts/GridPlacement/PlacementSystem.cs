using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;

    [SerializeField] private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    //[SerializeField] private GameObject gridVisualization;

    public static GridData towerData;
    private GridData blankData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObjects = new();

    // Timers for Puffer and Sword placement
    //public float swordTimer = -1;
    public float swordTimerMax = 5;
    //public float pufferTimer = -1;
    public float pufferTimerMax = 8;

    private void Start()
    {
        //swordTimer = -1;
        swordTimerMax = 5;
        //pufferTimer = -1;
        pufferTimerMax = 8;

        StopPlacement();
        towerData = new GridData();
        blankData = new GridData();
        previewRenderer = cellIndicator.GetComponent<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        //gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        if (database.objectsData[selectedObjectIndex].ID == 1 && inputManager.swordTimer > 0)
        {
            return;
        }
        if (database.objectsData[selectedObjectIndex].ID == 2 && inputManager.pufferTimer > 0)
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition - new Vector3(-32, -32, 0)); // the grid is offset by half  the cell size
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
        {
            return;
        }

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);

        placedGameObjects.Add(newObject);
        
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? blankData: towerData;
        selectedData.AddObjectAt(gridPosition,
            database.objectsData[selectedObjectIndex].Size,
            database.objectsData[selectedObjectIndex].ID,
            placedGameObjects.Count - 1);
        
        if(database.objectsData[selectedObjectIndex].ID == 1) // Swordfish placement
        {
            //swordTimer = swordTimerMax;
            inputManager.swordTimer = swordTimerMax;
        }
        if (database.objectsData[selectedObjectIndex].ID == 2) // Pufferfish placement
        {
            //pufferTimer = pufferTimerMax;
            inputManager.pufferTimer = pufferTimerMax;
        }

        StopPlacement();
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? blankData : towerData; // This is saying if the Id is zero, then it's "floor" which i put BLANKDATA intead

        return selectedData.CanPlaceOjbectat(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;

        //gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition - new Vector3 (-32, -32, 0)); // the grid is offset by half  the cell size

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

        /* // Timer was moved to InputManager because this system is only active while placing structures, so the timer stops
        // counts spawn timers down, which are only above zero after a unit is placed
        if(swordTimer > 0)
        {
            swordTimer -= Time.deltaTime;
        }
        if (pufferTimer > 0)
        {
            pufferTimer -= Time.deltaTime;
        }*/

    }
}
