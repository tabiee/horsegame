using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Maybe it's better via input system, and trigger an event than via a script
public class InventoryManager : MonoBehaviour
{
    public GameObject uiInventory;
    private bool isPaused = false;

    void Awake()
    {
        uiInventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                uiInventory.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                uiInventory.SetActive(false);
                isPaused = false;
            }
        }
    }
}
