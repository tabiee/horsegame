using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip soundEffect;

    private bool hasPlayedSound;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayedSound && collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundEffect);
            hasPlayedSound = true;
        }
    }
}