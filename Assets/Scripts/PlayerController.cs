using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Opting for diagonal movement, so Vertical for forward, backwards
    // A & D for rotation

    private float movementSpeed;
    public float defaultSpeed = 1f;
    public float waterSpeedMultiplier = 0.25f;
    public float bushSpeedMultiplier = 0.5f;
    public float rotationSpeed = 720f;

    private Rigidbody2D playerRb;
    public GameObject lightSource;
    public Tilemap tilemap;

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

    private void GetTileInfo()
    {
        Vector3Int playerTilePosition = tilemap.WorldToCell(transform.position);

        //Apply speed modifier: Grass is tile _223, water _78, bush _189

        TileBase playerTile = tilemap.GetTile(playerTilePosition);
        if (playerTile != null)
        {
            if (playerTile.name.Contains("78"))
            {
                movementSpeed = defaultSpeed * waterSpeedMultiplier;
            }
            else if (playerTile.name.Contains("189"))
            {
                movementSpeed = defaultSpeed * bushSpeedMultiplier;
            }
            else
            {
                movementSpeed = defaultSpeed;
            }
        }
    }

    private void SetPlayerVelocity()
    {

        GetTileInfo();

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
