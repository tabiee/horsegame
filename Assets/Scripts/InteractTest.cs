using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour, IInteractable, IEventRunner
{
    [SerializeField] private DialogueRunner dialogueRun;
    [SerializeField] private LoadDialogue loadDialogue;
    [SerializeField] private GameObject thisEvent;
    [SerializeField] private GameObject particle;
    public void Interact()
    {

        Debug.Log("I've been interacted with!");
        //gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //woo wee code that puts a reference to this object into DialogueRunner to be able to refer to this specific event
        dialogueRun.eventObject = thisEvent;

    }
    public void RunEvent()
    {
        if (loadDialogue.conditionTog == true)
        {
            Debug.Log("Running event after condition is true!");
            particle.SetActive(true);
        }
    }
}
