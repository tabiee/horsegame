using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Opting for diagonal movement, so Vertical for forward, backwards
    // A & D for rotation

    public float speed = 1f;
    private float rotationSpeed = 0.7f;
    private float movement;
    private float rotation;

    private void Update()
    {
        rotation = Input.GetAxis("Horizontal");
        movement = Input.GetAxis("Vertical"); 
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 direction = new Vector2(rotation * rotationSpeed, movement);
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        transform.up = direction;
    }
}
