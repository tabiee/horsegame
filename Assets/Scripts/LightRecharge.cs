using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class LightRecharge : MonoBehaviour
{
    [Header("Overheat Settings")]
    [SerializeField] private float heat = 0f;
    [SerializeField] private float maxHeat = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private Light2D ringLight;
    [SerializeField] private Light2D coneLight;
    [SerializeField] private LightCheck lightCheck;

    [Header("Cooldown Settings")]
    [SerializeField] private float cooldown = 5f;
    private float cdAllow;
    void Update()
    {
        slider.value = CalculatePercentage();

        if (heat != maxHeat)
        {
            if (Input.GetMouseButton(0) && heat < maxHeat && Time.time > cdAllow)
            {
                slider.gameObject.SetActive(true);
                ringLight.enabled = false;
                coneLight.enabled = true;
                lightCheck.enabled = true;
                heat++;
            }
            else
            {
                if (Time.time > cdAllow)
                {
                    ringLight.enabled = true;
                }
                coneLight.enabled = false;
                lightCheck.enabled = false;
                if (heat > 0f)
                {
                    heat--;
                }
                else
                {
                    slider.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            cdAllow = Time.time + cooldown;
            heat--;

            ringLight.enabled = false;
            coneLight.enabled = false;
            lightCheck.enabled = false;
            slider.gameObject.SetActive(false);
        }
    }
    float CalculatePercentage()
    {
        float currHeat = heat;
        float overHeat = maxHeat;
        return currHeat / overHeat;
    }
}
