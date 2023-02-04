using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlt : MonoBehaviour
{
    [SerializeField] private MovementAlt moveAlt;
    [SerializeField] private float slowedSpeed = 0.3f;
    private SpriteRenderer sprite;
    public Animator animator;
    private void Awake()
    {
        moveAlt = GetComponentInParent<MovementAlt>();
        animator = transform.parent.GetComponentInChildren<Animator>();
        sprite = transform.parent.Find("Sprite").GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //rotate towards light
        Vector3 lightDirection = (mousePosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);

        //get mouse direction by getting it's position and converting from world to local space
        Vector2 mouseDir = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //draw a line from the direction of movement to mouse direction and see wtf is up
        if (Vector2.Dot(moveAlt.moveDir.normalized, mouseDir.normalized) < 0)
        {
            Debug.Log("Opposite mouse");
            moveAlt.moveSpeed = slowedSpeed;
        }
        else
        {
            moveAlt.moveSpeed = moveAlt.defaultSpeed;
        }

        var angleZ = transform.eulerAngles.z;
        if (angleZ >= 0 && angleZ <= 180)
        {
            //Debug.Log(angleZ);
            Debug.Log("Left Side");
        }


        animator.SetFloat("Horizontal", mouseDir.x);
        animator.SetFloat("Vertical", mouseDir.y);
        animator.SetFloat("Speed", moveAlt.moveDir.sqrMagnitude);

        if (mouseDir.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        Debug.Log("mouseDir is " + mouseDir);


        //saving this in case i need it at some point

        /* Vector3 mousePosition = Input.mousePosition;
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
         //mousePosition.z = 0;

         Vector3 lightDirection = (mousePosition - transform.position).normalized;

         transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);

         //calculate the Y and X position of the mouse relative to the player rotation
         float mouseY = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).y - transform.parent.localRotation.y;
         float mouseX = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).x - transform.parent.localRotation.x;

         //checks mouse position relative to player rotation is "above" and "not too far left" and "not too far right"
         if (mouseY > 0 && mouseX > -0.45 && mouseX < 0.45)
         {
             Debug.Log("Up of player!");
         }
         //testing to check if mouse position is "below"
         if (mouseY < 0)
         {
             Debug.Log("Down of player!");
         } */
    }
}
