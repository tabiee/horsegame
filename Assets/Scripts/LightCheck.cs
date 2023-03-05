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

    [Header("General")]
    public EnemyInLight hitEnemy;
    public bool once = false;
    public bool toggle = false;

    private void Update()
    {
        //Debug.Log("hitEnemy data is: " + hitEnemy);

    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        hitEnemy = other.GetComponent<EnemyInLight>();

        if (hitEnemy != null)
        {
            hitEnemy.inLight = true;

            if (other.gameObject.tag == "Kelpie" && once == false)
            {

                StartCoroutine(hitEnemy.KelpieLoop());
            }
            if (other.gameObject.tag == "Limsect" && once == false)
            {
                StartCoroutine(hitEnemy.LimsectLoop());
            }
        }
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {

        hitEnemy = other.GetComponent<EnemyInLight>();
        if (hitEnemy != null)
        {
            hitEnemy.inLight = true;

            if (other.gameObject.tag == "Kelpie")
            {
                Debug.Log(other.name + " Entered!");
                StartCoroutine(hitEnemy.KelpieLoop());
            }
            if (other.gameObject.tag == "Limsect")
            {
                StartCoroutine(hitEnemy.LimsectLoop());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        hitEnemy = other.GetComponent<EnemyInLight>();
        if (hitEnemy != null)
        {
            hitEnemy.inLight = false;

            if (other.gameObject.tag == "Kelpie")
            {
                Debug.Log(other.name + " Exited!");
                StopCoroutine(hitEnemy.KelpieLoop());
            }
            if (other.gameObject.tag == "Limsect")
            {
                StopCoroutine(hitEnemy.LimsectLoop());
            }
        }
    }

    /* private void OnEnable()
     {
         if (hitEnemy != null && hitEnemy.gameObject.tag == "Kelpie")
         {
             hitEnemy.inLight = true;
             StartCoroutine(hitEnemy.KelpieLoop());
         }
         if (hitEnemy != null && hitEnemy.gameObject.tag == "Limsect")
         {
             hitEnemy.inLight = true;
             StartCoroutine(hitEnemy.LimsectLoop());
         }
     }
     private void OnDisable()
     {
         if (hitEnemy != null && hitEnemy.gameObject.tag == "Kelpie")
         {
             hitEnemy.inLight = false;
             StopCoroutine(hitEnemy.KelpieLoop());
         }
         if (hitEnemy != null && hitEnemy.gameObject.tag == "Limsect")
         {
             hitEnemy.inLight = false;
             StopCoroutine(hitEnemy.LimsectLoop());
         }
     }*/
}
