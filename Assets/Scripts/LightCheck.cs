using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LightCheck : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private bool inLight;
    [SerializeField] private float scitterSpeed = 0.5f;
    [SerializeField] private float sideSpeed = 3f;

    private void Update()
    {
        if (inLight)
        {
            //move right and forward from local space when in light
            aiPath.transform.localPosition += (transform.right * sideSpeed + transform.up * scitterSpeed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            //if in light, hide sprite, change bool that does speed and show splash sprite
            aiPath = other.GetComponent<AIPath>();
            Debug.Log("Kelpie is in the cone!");
            inLight = true;

            other.GetComponent<AIPath>().enabled = false;
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<AIPath>() != null && other.name == "evil")
        {
            Debug.Log("Kelpie left the cone!");
            inLight = false;

            other.GetComponent<AIPath>().enabled = true;
            other.GetComponent<SpriteRenderer>().enabled = true;
            other.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
