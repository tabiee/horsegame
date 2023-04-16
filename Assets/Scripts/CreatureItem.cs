using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureItem : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float pitchChange = 1.5f;
    [SerializeField] private GameObject triggerZone;
    public void Interact()
    {
        audioSource.clip = audioClip;
        audioSource.pitch = pitchChange;
        audioSource.enabled = false;
        audioSource.spatialBlend = 0f;

        triggerZone.SetActive(true);
    }
}
