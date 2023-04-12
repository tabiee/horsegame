using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureItem : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject triggerZone;
    public void Interact()
    {
        audioSource.enabled = false;

        triggerZone.SetActive(true);
    }
}
