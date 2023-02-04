using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Transform playerTransform;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //create a direction vector, don't forget to add .normalized to match it to a distance of 1 to prevent diagonal mistakes (Thank you mr pythagoras)
        Vector3 direction = (mousePosition - playerTransform.position).normalized;

        // Limits rotation in 40 degrees left and right facing forward
        float angle = Vector2.SignedAngle(playerTransform.up, direction);
        angle = Mathf.Clamp(angle, -40, 40);

        //Apply clamped rotation
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
