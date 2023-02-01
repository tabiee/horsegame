using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LightCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            Debug.Log("Kelpie is in the cone!");
            other.GetComponent<AIPath>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            Debug.Log("Kelpie left the cone!");
            other.GetComponent<AIPath>().enabled = true;
        }
    }
    void Update()
    {
        
    }
}
