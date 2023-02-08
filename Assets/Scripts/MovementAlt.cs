using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAlt : MonoBehaviour
{
    public float moveSpeed = 0.75f;
    public float defaultSpeed;
    public Vector2 moveDir;
    [SerializeField] private Rigidbody2D rb;
    public Vector2 latestDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = moveSpeed;
    }

    private void Update()
    {
        //input for Update
        ProcessInputs();
    }
    private void FixedUpdate()
    {
        //physics for FixedUpdate
        Move();
    }
    void ProcessInputs()
    {
        Debug.Log("LatestDir is: " + latestDir);

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir != Vector2.zero)
        {
            latestDir.x = moveX;
            latestDir.y = moveY;
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
