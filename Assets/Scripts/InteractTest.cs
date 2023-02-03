using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("I've been interacted with!");
    }
}
