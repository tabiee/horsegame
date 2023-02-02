using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject lightSource;
    public PlayerController playerController;

    void Update()
    { 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Vector3 lightDirection = (mousePosition - transform.position).normalized;

        Vector3 playerDirection = new Vector3(playerController.transform.rotation.x, playerController.transform.rotation.y);

        Debug.Log(playerDirection);

        lightSource.transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);
    }
}
