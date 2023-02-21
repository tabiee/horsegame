using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadDialogue : MonoBehaviour, IInteractable
{
    [Header("Text Values")]
    public string[] lines;
    [SerializeField] private Sprite artSprite;
    private GameObject playerDialogue;
    private GameObject playerTextbox;
    private Image loadSprite;

    [Header("Player Scripts")]
    private RayInteraction rayInt;
    private MovementAlt move;
    private DialogueRunner dialogueRun;
    private TextMeshProUGUI buttonSelect1;
    private TextMeshProUGUI buttonSelect2;

    [Header("Choice Values (Optional)")]
    public string[] choiceLines1;
    public string[] choiceLines2;
    public string selectText1;
    public string selectText2;

    [Header("Condition Values (Optional)")]
    public string[] condLines;
    public bool conditionTog = false;
    public GameObject untiedEvent;
    public void Start()
    {
        //grab all the player things
        playerDialogue = GameObject.Find("Dialogue");
        playerTextbox = playerDialogue.transform.Find("Box").gameObject;
        rayInt = GameObject.Find("Player").GetComponent<RayInteraction>();
        move = GameObject.Find("Player").GetComponent<MovementAlt>();
        dialogueRun = playerTextbox.transform.Find("Text").GetComponent<DialogueRunner>();
        buttonSelect1 = playerDialogue.transform.Find("Choice1").GetComponentInChildren<TextMeshProUGUI>();
        buttonSelect2 = playerDialogue.transform.Find("Choice2").GetComponentInChildren<TextMeshProUGUI>();
        loadSprite = playerDialogue.transform.Find("Image").GetComponent<Image>();
    }
    public void Interact()
    {
        DisablePlayer();
        //if there's no condition, load normal dialogue
        if (condLines == null)
        {
            //if there's no choice, load single dialogue
            if (choiceLines1.Length == 0)
            {
                ActivateDialogue();
            }
            //else load multiple dialogues
            else
            {
                ActivateMultiDialogue();
            }
        }
        //else check if condition is true
        else if (conditionTog == true)
        {
            ActivateCondDialogue();
        }
        //if condition isnt fulfilled, run normal dialogue
        else
        {
            //if there's no choice, load single dialogue
            if (choiceLines1.Length == 0)
            {
                ActivateDialogue();
            }
            //else load multiple dialogues
            else
            {
                ActivateMultiDialogue();
            }
        }
    }
    public void ActivateDialogue()
    {
        playerTextbox.SetActive(true);
        loadSprite.gameObject.SetActive(true);
        loadSprite.sprite = artSprite;
        dialogueRun.lines = lines;
        dialogueRun.StartDialogue();
    }
    public void ActivateMultiDialogue()
    {
        playerTextbox.SetActive(true);
        loadSprite.gameObject.SetActive(true);
        loadSprite.sprite = artSprite;
        dialogueRun.lines = lines;
        dialogueRun.choiceLines1 = choiceLines1;
        dialogueRun.choiceLines2 = choiceLines2;
        buttonSelect1.text = selectText1;
        buttonSelect2.text = selectText2;
        dialogueRun.StartDialogue();
    }
    public void ActivateCondDialogue()
    {
        playerTextbox.SetActive(true);
        loadSprite.gameObject.SetActive(true);
        loadSprite.sprite = artSprite;
        dialogueRun.lines = condLines;
        dialogueRun.StartDialogue();
    }
    public void DisablePlayer()
    {
        rayInt.enabled = false;
        move.enabled = false;
    }
}
