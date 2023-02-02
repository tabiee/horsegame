using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Opting for diagonal movement, so Vertical for forward, backwards
    // A & D for rotation

    public float movementSpeed = 1f;
    public float rotationSpeed = 720f; //2 full circles per 1unit ingame?

    private Rigidbody2D playerRb;
    public Transform parentTransform;
    public GameObject lightSource;


    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    private Quaternion lookDirection;
    private Quaternion targetRotation;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
        PointFlashLightAtMouse();
    }

    private void SetPlayerVelocity()
    {
        //Makes it less jerky, starts with itself? takes the input from OnMove(), references its speed, and add time interval if change in seconds.
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        playerRb.velocity = smoothedMovementInput * movementSpeed;
    }

    private void RotateInDirectionOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            lookDirection = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            playerRb.MoveRotation(lookDirection); //MoveRotation is new rotation method for rigidbody?
        }
    }

    private void PointFlashLightAtMouse()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 playerPos = transform.position;

        Vector3 lightDirection = (mousePosition - playerPos).normalized;

        Quaternion fullRotation = Quaternion.LookRotation(Vector3.forward, lightDirection);

        float relativeRotation = parentTransform.rotation.eulerAngles.z - fullRotation.eulerAngles.z;
        relativeRotation = Mathf.Clamp(relativeRotation, 40, -40);
        Quaternion clampedRotation = Quaternion.Euler(0, 0, relativeRotation);

        if(parentTransform.rotation != parentTransform.rotation)
        {
            lightSource.transform.rotation = fullRotation;
        }
        else
        {
            lightSource.transform.rotation = clampedRotation;
        }


        Debug.Log("direction: " + lightDirection);
        Debug.Log("parentTranfsorm.fwd: " + parentTransform.forward);
        Debug.Log("fullRotation.eul.z: " + fullRotation.eulerAngles.z);
        Debug.Log("direction: " + lightDirection);
        Debug.Log("Clamped.eul.z: " + clampedRotation.eulerAngles.z);
        Debug.Log("light: " + lightSource.transform.rotation.eulerAngles.z);
    }

    private void OnMove(InputValue inputValue) //Automatic method to collect stuff from new inputsystem, onMove is a Unity EventTrigger
    {
        movementInput = inputValue.Get<Vector2>(); //Using the inputsystem namespace, this gets the value of all axis input according to th mapping you chose
    }

    //float zAngle = fullRotation.eulerAngles.z;
    //if (fullRotation.eulerAngles.z > 40 && fullRotation.eulerAngles.z < 180)
    //{
    //    zAngle = 40;
    //}
    //else if (fullRotation.eulerAngles.z > 180 && fullRotation.eulerAngles.z < 320)
    //{
    //    zAngle = 320;
    //}
    //Quaternion clampedRotation = Quaternion.Euler(0, 0, zAngle);
}
