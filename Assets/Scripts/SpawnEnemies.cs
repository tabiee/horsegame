using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyAmount = 7;
    [SerializeField] private GameObject[] spawnLocations;
    private int spawnIndex;
    private bool once = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(SpawnLoop());
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
        Debug.Log("spawnloop ended");
    }
    private void Update()
    {
    }
}
