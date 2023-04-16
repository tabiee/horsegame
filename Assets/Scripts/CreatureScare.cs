using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScare : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite chaseSprite;
    [SerializeField] private int chaseDuration = 5;
    [SerializeField] private GameObject poofParticles;

    private bool once;
    private int timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && once == false)
        {
            spriteRenderer.sprite = chaseSprite;
            audioSource.enabled = true;

            StartCoroutine(Chase());
            once = true;
        }
    }

    private IEnumerator Chase()
    {
        Debug.Log("chase start");
        while (timer <= chaseDuration)
        {
            timer++;
            Debug.Log("chasing! " + timer);

            spriteRenderer.gameObject.transform.Translate(0, -0.04f, 0);
            yield return null;
        }
        Debug.Log("chase end");
        spriteRenderer.gameObject.SetActive(false);
        Instantiate(poofParticles, spriteRenderer.transform.position, Quaternion.identity);
    }
}
