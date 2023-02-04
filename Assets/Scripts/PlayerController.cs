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
            
            //MoveRotation is new rotation method for rigidbody?
            playerRb.MoveRotation(lookDirection);
        }
    }

    //Automatic method to collect stuff from new inputsystem, onMove is a Unity EventTrigger
    private void OnMove(InputValue inputValue)
    {
        //Using the inputsystem namespace, this gets the value of all axis input according to th mapping you chose
        movementInput = inputValue.Get<Vector2>(); 
    }
}
