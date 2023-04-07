using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    [SerializeField] private float fadeoutTime = 3f;
    [SerializeField] private TextMeshProUGUI textDisplay;
    public void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float currentTime = 0f;
        while (currentTime < fadeoutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeoutTime);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
