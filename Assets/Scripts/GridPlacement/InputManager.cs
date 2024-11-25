using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField] private LayerMask placementLayermask; // so far i haven't implemented the mask

    public event Action OnClicked, OnExit;

    public float gameTimer;
    [SerializeField] TextMeshProUGUI gameTimerText;
    public float swordTimer = -1;
    public float pufferTimer = -1;
    [SerializeField] TextMeshProUGUI swordTextTimer;
    [SerializeField] TextMeshProUGUI pufferTextTimer;

    private void Start()
    {
        gameTimer = 180;
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        gameTimerText.text = "Survive for " + Mathf.Round(gameTimer);
        if(gameTimer < 0)
        {
            // Win game state
            SceneManager.LoadScene(3);
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            OnExit?.Invoke();
        }

        if (swordTimer > 0)
        {
            swordTimer -= Time.deltaTime;
            swordTextTimer.text = "Cooldown: " + Mathf.Round(swordTimer) + "s";
        }
        if (pufferTimer > 0)
        {
            pufferTimer -= Time.deltaTime;
            pufferTextTimer.text = "Cooldown: " + Mathf.Round(pufferTimer) + "s";
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        // Works much better than previous tries
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;

        /*Vector3 mousePos = Input.mousePosition ;
        Debug.Log(mousePos);
        return mousePos;*/

        /*mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics2D.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
            Debug.Log("Raycast");
        }
        return lastPosition;*/
    }
}
