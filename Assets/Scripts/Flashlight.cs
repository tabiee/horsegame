using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    void Update()
    { 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //mousePosition.z = 0;

        Vector3 lightDirection = (mousePosition - transform.position).normalized;

        Vector3 playerDirection = new Vector3(0, 0, transform.parent.rotation.z);


        //witness my failed attempts to make this work

        //Debug.Log(playerDirection);
        //Debug.Log("Light rotation Z is: " + lightSource.transform.rotation.z);
        //Debug.Log("Light dir is: " + lightDirection);

        //lightSource.transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);

        /*if (lightSource.transform.eulerAngles.z > -45 && lightSource.transform.eulerAngles.z < 45)
        {
            lightSource.transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);
        }

        //Debug.Log(transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).y - transform.parent.localPosition.y);
        */

        //Debug.Log(transform.eulerAngles.z);
        //Debug.Log(transform.rotation.z);

       /* if (transform.rotation.z < -0.45f)
        {
            transform.eulerAngles = new Vector3(0, 0,-44f);
        }
        if (transform.rotation.z > 0.45f)
        {
            transform.eulerAngles = new Vector3(0, 0, 44f);
        }*/



        //calculate the Y and X position of the mouse relative to the player rotation
        float mouseY = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).y - transform.parent.localRotation.y;
        float mouseX = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)).x - transform.parent.localRotation.x;

        //checks mouse position relative to player rotation is "above" and "not too far left" and "not too far right"
        if (mouseY > 0 && mouseX > -0.45 && mouseX < 0.45)
        {
            Debug.Log("Up of player!");
            transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);
        }
        //testing to check if mouse position is "below"
        if (mouseY < 0)
        {
            Debug.Log("Down of player!");
        }
    }
}
