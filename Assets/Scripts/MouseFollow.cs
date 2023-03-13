using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - cam.transform.position.z));
        float angle = Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));

    }
}
