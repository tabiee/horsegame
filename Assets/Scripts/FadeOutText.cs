using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    [SerializeField] private float fadeoutTime = 3f;
    public TextMeshProUGUI textDisplay;
    public void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float currentTime = 0f;
        while (currentTime < fadeoutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeoutTime);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;

            //Debug.Log("while loop running");
            //Debug.Log("currentTime " + currentTime);
            //Debug.Log("alpha " + alpha);

            yield return null;
        }
        //yield break;
    }
    public void TriggerFade()
    {
        StartCoroutine(FadeOut());
    }
}
