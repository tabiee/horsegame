using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour, IInteractable
{
    [SerializeField] private bool once = false;
    [SerializeField] private DialogueRunner dialogueRun;
    [SerializeField] private LoadDialogue loadDialogue;
    [SerializeField] private GameObject thisEvent;
    public void Interact()
    {
        if (once == false)
        {
            //separate object that will approve a condition for the 2nd set of dialogue on an npc
            //runs once only
            Debug.Log("Once! I've been interacted with!");
            gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            loadDialogue.conditionTog = true;
            once = true;
        }
    }
    void Update()
    {

    }
}
