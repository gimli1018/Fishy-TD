using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : MonoBehaviour
{
    private int gameObjectIndex = -1;
    Grid grid;
    ObjectsDatabaseSO database;
    GridData towerData;

    public RemovingState(Grid grid,
                         ObjectsDatabaseSO database,
                         GridData towerData)
    {
        this.grid = grid;
        this.database = database;
        this.towerData = towerData;
    }
}
