using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteraction : MonoBehaviour
{
    [SerializeField] private RaycastHit2D hitData;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float distance = 0.4f;

    public bool activeLocation = true;
    private Vector2 mousePos;
    private Vector2 dir;
    void Update()
    {
        //grab mouse position
        if (activeLocation == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mousePos - (Vector2)transform.position;
        }

        //show the ray
        Debug.DrawRay(transform.position, dir.normalized, Color.red, 0.1f, false);

        //shoot a ray from player position to mouse position, only hit the specified layer
        hitData = Physics2D.Raycast(transform.position, dir.normalized, distance, interactionLayer);

        //if it hit something, run stuff
        if (hitData.collider != null)
        {
            //Debug.Log(hitData.collider.gameObject.name);

            //get the inherited interfaces from the hit object
            Component[] interactedObjects = hitData.transform.GetComponents(typeof(IInteractable));
            foreach (IInteractable interactedObject in interactedObjects)
            {

                if (interactedObject != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Debug.Log("E pressed!");
                        //use the method from the object, regardless of which script is using that method
                        interactedObject.Interact();
                    }
                }
            }
        }
    }
}
