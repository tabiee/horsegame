using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteraction : MonoBehaviour
{
    public RaycastHit2D hitData;
    public LayerMask interactionLayer;
    public float distance = 10f;

    void Update()
    {
        //Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);


        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)transform.position;

        Debug.DrawRay(transform.position, dir.normalized, Color.red, 0.1f, false);

        hitData = Physics2D.Raycast((Vector2)transform.position, dir.normalized, distance, interactionLayer);

        if (hitData.collider != null)
        {
            Debug.Log(hitData.collider.gameObject.name);
            var interactedObject = hitData.transform.GetComponent<IInteractable>();

            if (interactedObject != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E pressed!");
                    interactedObject.Interact();
                }
            }
        }
    }
}
