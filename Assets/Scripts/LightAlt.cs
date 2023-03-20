using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlt : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float slowedSpeed = 0.6f;
    [SerializeField] private float wobbleSpeed = 1f;
    [SerializeField] private float wobbleStrength = 0.025f;
    [Header("Components")]
    [SerializeField] private MovementAlt moveAlt;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 mouseDir;
    [SerializeField] private LightCheck lightCheck;
    private void Awake()
    {
        //grab everything i need from the sprite object
        moveAlt = GetComponentInParent<MovementAlt>();
        animator = transform.GetComponentInParent<Animator>();
        sprite = transform.GetComponentInParent<SpriteRenderer>();
        lightCheck = GetComponent<LightCheck>();
    }
    void Update()
    {
        //wobble
        float y = Mathf.Sin(Time.time * wobbleSpeed) * wobbleStrength;

        transform.localPosition = new Vector3(0, y, transform.localPosition.z);

        //run animations
        Animate();

        //handle light rotation & movement speed
        if (lightCheck.toggle == true)
        {
            RotateLight();
        }
        else
        {
            //debug for OnTriggerExit not firing when the cone is disabled
            RotateLight();
            transform.eulerAngles = new Vector3(90, 0, transform.eulerAngles.z);
        }
    }
    void RotateLight()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //rotate towards light
        Vector3 lightDirection = (mousePosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lightDirection);

        //get mouse direction by getting it's position and converting from world to local space
        mouseDir = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //draw a line from the direction of movement to mouse direction and see wtf is up
        if (Vector2.Dot(moveAlt.moveDir.normalized, mouseDir.normalized) < 0)
        {
            //Debug.Log("Opposite mouse");
            moveAlt.speedSlowed = slowedSpeed;
        }
        else
        {
            moveAlt.speedSlowed = 1f;
        }

        var angleZ = transform.eulerAngles.z;
        if (angleZ >= 0 && angleZ <= 180)
        {
            //Debug.Log(angleZ);
            //Debug.Log("Left Side");
        }
    }
    void Animate()
    {
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
    }
}
