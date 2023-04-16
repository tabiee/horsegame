using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFInventory;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyAmount = 7;
    [SerializeField] private GameObject[] spawnLocations;
    [SerializeField] private string itemName;
    [SerializeField] private int freezeTimer;
    public SFInventoryManager sFInventoryManager;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float audioPitch = 1;

    private MovementAlt move;
    private int spawnIndex;
    private bool once = false;

    private void Start()
    {
        move = GameObject.Find("Player").GetComponent<MovementAlt>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (once == false)
            {
                once = true;

                bool bottlePresent = false;
                foreach (SFInventoryCell cell in sFInventoryManager.inventoryCells)
                {
                    if (itemName != "no")
                    {
                        if (cell.item != null && cell.item.name == itemName && cell.itemsCount >= 1)
                        {
                            bottlePresent = true;
                            break;
                        }
                    }
                }

                if (bottlePresent)
                {
                    Debug.Log(itemName + " is present in the inventory with at least 1 amount.");
                    StartCoroutine(SpawnLoop());
                }
                else
                {
                    Debug.Log(itemName + " is not present in the inventory or its amount is less than 1.");
                    if (itemName == "no")
                    {
                        StartCoroutine(SpawnLoop());
                    }
                    else
                    {
                        once = false;
                    }
                }
            }
        }
    }
    private IEnumerator SpawnLoop()
    {
        Debug.Log("spawnloop started");
        for (var i = 0; i < enemyAmount; i++)
        {
            spawnIndex = Random.Range(0, spawnLocations.Length);
            Instantiate(enemy, spawnLocations[spawnIndex].transform.position, Quaternion.identity);
            Debug.Log("spawnloop running " + spawnIndex);
            yield return null;
        }
        StartCoroutine(FreezePlayer());
        Debug.Log("spawnloop ended");
    }
    private IEnumerator FreezePlayer()
    {
        for (var i = 0; i < freezeTimer; i++)
        {
            DisablePlayer();

            yield return null;
        }

        EnablePlayer();
        audioSource.clip = audioClip;
        audioSource.pitch = audioPitch;
        audioSource.Play();
    }

    private void DisablePlayer()
    {
        move.rb.velocity = Vector2.zero;
        move.moveDir.x = 0;
        move.moveDir.y = 0;
        move.enabled = false;
        //lightAlt.enabled = false;
    }
    private void EnablePlayer()
    {
        move.enabled = true;
        //lightAlt.enabled = false;
    }
}
