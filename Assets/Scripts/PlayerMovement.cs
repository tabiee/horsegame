using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Opting for diagonal movement, so Vertical for forward, backwards
    // A & D for rotation

    public float movementSpeed = 1f;
    public float rotationSpeed = 720f;

    private Rigidbody2D playerRb;

    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

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
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            playerRb.MoveRotation(rotation); //MoveRotation is new rotation method for rigidbody?
        }
    }

    private void OnMove(InputValue inputValue) //Automatic method to collect stuff from new inputsystem, onMove is a Unity EventTrigger
    {
        movementInput = inputValue.Get<Vector2>(); //Using the inputsystem namespace, this gets the value of all axis input according to th mapping you chose
    }
}
