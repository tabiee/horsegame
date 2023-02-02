using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggle : MonoBehaviour
{
    private  Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }
    
    public void ToggleLight()
    {
        light2D.enabled = !light2D.enabled;
    }
}
