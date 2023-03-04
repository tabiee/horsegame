using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY, startposX, startposY;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float xTemp = cam.transform.position.x * 1 - parallaxEffect;
        float xDist = cam.transform.position.x * parallaxEffect;
        float yTemp = cam.transform.position.y * 1 - parallaxEffect;
        float yDist = cam.transform.position.y * parallaxEffect;
        transform.position = new Vector3(startposX + xDist, startposY + yDist, transform.position.z);

        if (xTemp > startposX + lengthX) startposX += lengthX;
        else if (xTemp < startposX - lengthX) startposX -= lengthX;
        if (yTemp > startposY + lengthY) startposY += lengthY;
        else if (yTemp < startposY - lengthY) startposY -= lengthY;
    }
}
