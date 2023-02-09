using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadDialogue : MonoBehaviour, IInteractable
{
    [Header("Text Values")]
    public string[] lines;
    [SerializeField] private GameObject playerDialogue;
    [SerializeField] private GameObject playerTextbox;
    [SerializeField] private TextMeshProUGUI playerTextGUI;

    [Header("Player Scripts")]
    [SerializeField] private RayInteraction rayInt;
    [SerializeField] private MovementAlt move;
    [SerializeField] private DialogueRunner dialogueRun;

    public void Start()
    {
        //grab all the player things
        playerDialogue = GameObject.Find("Dialogue");
        playerTextbox = playerDialogue.transform.Find("Box").gameObject;
        rayInt = GameObject.Find("Player").GetComponent<RayInteraction>();
        move = GameObject.Find("Player").GetComponent<MovementAlt>();
        dialogueRun = playerTextbox.transform.Find("Text").GetComponent<DialogueRunner>();
    }
    public void Interact()
    {
        DisablePlayer();
        Debug.Log("I've been interacted with!");
        gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Invoke("ActivateDialogue", 0.1f);
    }
    public void ActivateDialogue()
    {
        playerTextbox.SetActive(true);
        dialogueRun.lines = lines;
        dialogueRun.StartDialogue();
    }
    public void DisablePlayer()
    {
        rayInt.enabled = false;
        move.enabled = false;
    }
}
