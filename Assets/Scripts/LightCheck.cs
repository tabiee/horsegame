using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LightCheck : MonoBehaviour
{
    //[Header("Trigger Area")]
    //[SerializeField] private AIPath aiPath;
    //[SerializeField] private bool inLight;
    //[SerializeField] private float scitterSpeed = 0.65f;
    //[SerializeField] private float sideSpeed = 0.85f;

    [Header("Raycast")]
    public RaycastHit2D hitData;
    public LayerMask playerLayer;
    public float distance = 25f;

    //private bool once = false;
    //private int random;

    public bool toggle = false;

    private void Update()
    {
        //ShadowCheck();
        //is it in light and not behind anything?

        /* if (inLight && ShadowCheck() == true && toggle == true)
         {
             if (once == false)
             {
                 once = true;
                 random = Random.Range(0, 2);
             }
             var playerDir = aiPath.transform.localPosition - transform.parent.position;
             switch (random)
             {
                 case 0:
                     //move right (x) and forward from local space when in light
                     aiPath.transform.localPosition += (transform.right * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                     break;
                 case 1:
                     aiPath.transform.localPosition += (-transform.right * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                     break;
                 case 2:
                     aiPath.transform.localPosition += (-transform.right * sideSpeed + playerDir * scitterSpeed) * Time.deltaTime;
                     break;
             }
             aiPath.GetComponent<AIPath>().enabled = false;
             aiPath.GetComponent<SpriteRenderer>().enabled = false;
             aiPath.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = true;

         }
         else if (hitData.collider != null)
         {
             once = false;
             aiPath.GetComponent<AIPath>().enabled = true;
             aiPath.GetComponent<SpriteRenderer>().enabled = true;
             aiPath.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = false;
         }
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name + " Entered!");

        //foreach (Collider2D enemyContact in other)
        if (other.GetComponent<EnemyInLight>() != null)
        {
            other.GetComponent<EnemyInLight>().inLight = true;
        }

        /*
        if (other.GetComponent<AIPath>() != null && other.tag == "evil")
        {
            //if in light, hide sprite, change bool that does speed and show splash sprite
            aiPath = other.GetComponent<AIPath>();
            Debug.Log("Kelpie is in the cone!");
            inLight = true;
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.name + " Exited!");

        if (other.GetComponent<EnemyInLight>() != null)
        {
            other.GetComponent<EnemyInLight>().inLight = false;
        }

        /*
        if (other.GetComponent<AIPath>() != null && other.tag == "evil")
        {
            Debug.Log("Kelpie left the cone!");
            inLight = false;
        }*/
    }
    /* public bool ShadowCheck()
     {
         if (hitData.collider != null)
         {
             //Debug.Log(hitData.collider.gameObject.name);
         }

         if (aiPath != null)
         {
             Ray2D ray = new Ray2D(transform.parent.position, aiPath.transform.position - transform.parent.position);

             //show the ray
             Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.1f, false);

             //shoot a ray from player position to kelpie position, only hit the specified layer
             hitData = Physics2D.Raycast(ray.origin, ray.direction, distance, ~playerLayer);
         }

         //if it hit something, run stuff
         if (hitData.collider != null && hitData.collider.tag == "evil")
         {
             return true;
         }
         else
         {
             return false;
         }
     }*/
}
