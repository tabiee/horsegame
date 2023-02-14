using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour, IInteractable, IEventRunner
{
    [SerializeField] private DialogueRunner dialogueRun;
    [SerializeField] private GameObject thisEvent;
    public void Interact()
    {
        Debug.Log("I've been interacted with!");
        gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //woo wee code that puts a reference to this object into DialogueRunner to be able to refer to this specific event
        dialogueRun.eventObject = thisEvent;
        Debug.Log("Assigned!");
    }
    public void RunEvent()
    {
        Debug.Log("Running event!");
    }
}
