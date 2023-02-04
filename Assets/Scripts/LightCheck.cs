using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LightCheck : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private bool inLight;
    [SerializeField] private float scitterSpeed = 0.5f;

    private void Update()
    {
        if (inLight)
        {
            aiPath.transform.localPosition += (transform.right * 2f + transform.up) * Time.deltaTime * scitterSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            aiPath = other.GetComponent<AIPath>();
            Debug.Log("Kelpie is in the cone!");
            inLight = true;

            other.GetComponent<AIPath>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            Debug.Log("Kelpie left the cone!");
            inLight = false;

            other.GetComponent<AIPath>().enabled = true;
        }
    }
}
