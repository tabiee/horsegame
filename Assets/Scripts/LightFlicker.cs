using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{

    [SerializeField] private Light2D _light;
    [SerializeField] private float _baseIntensity = 1f;
    [SerializeField] private float _intensityRange = 0.2f;
    [SerializeField] private float _intensityTimeMin = 0.01f;
    [SerializeField] private float _intensityTimeMax = 0.5f;
    [SerializeField] private bool flickIntensity = true;

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(_baseIntensity - _intensityRange, _baseIntensity + _intensityRange);
                _light.intensity = r;
                t = Random.Range(_intensityTimeMin, _intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
}
