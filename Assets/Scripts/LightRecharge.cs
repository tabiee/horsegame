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
    [SerializeField] private PolygonCollider2D lightCollider;
    [SerializeField] private LightCheck lightCheck;
    [SerializeField] private ParticleSystem particles;

    [Header("Cooldown Settings")]
    [SerializeField] private float cooldown = 5f;
    private float cdAllow;
    private bool once = false;
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
                //lightCollider.gameObject.transform.position = new Vector3(0, lightCollider.gameObject.transform.position.y, startPos);
                lightCheck.toggle = true;
                //lightCheck.enabled = true;
                heat++;
                ParticleEffect();
            }
            else
            {
                if (Time.time > cdAllow)
                {
                    ringLight.enabled = true;
                }
                coneLight.enabled = false;
                //lightCollider.enabled = false;
                //lightCollider.gameObject.transform.position = new Vector3(0, lightCollider.gameObject.transform.position.y, startPos - 5);
                lightCheck.toggle = false;
                //lightCheck.enabled = false;
                if (heat > 0f)
                {
                    heat--;
                }
                else
                {
                    slider.gameObject.SetActive(false);
                }
                once = false;
            }
        }
        else
        {
            cdAllow = Time.time + cooldown;
            heat--;

            ringLight.enabled = false;
            //lightCollider.enabled = false;
            coneLight.enabled = false;
            lightCheck.toggle = false;
            slider.gameObject.SetActive(false);
            once = false;
        }
    }
    void ParticleEffect()
    {
        if (once == false)
        {
            once = true;
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
    float CalculatePercentage()
    {
        float currHeat = heat;
        float overHeat = maxHeat;
        return currHeat / overHeat;
    }
}
