using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueRunner : MonoBehaviour
{
    //general text stuff
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI textGUI;
    [SerializeField] private GameObject textBox;
    [SerializeField] private float textSpeed;
    [SerializeField] private int index;
    public string[] lines;

    //refs to object for activation/deactivation
    [Header("Scripts & Buttons")]
    [SerializeField] private RayInteraction rayInt;
    [SerializeField] private MovementAlt move;
    [SerializeField] private Button choiceButton1;
    [SerializeField] private Button choiceButton2;
    [SerializeField] private Image image;

    //the LoadDialogue you interact with will load these values into here if multiple choice is available
    [Header("Choice Values")]
    [SerializeField] private bool choicePause = false;
    [SerializeField] private bool choiceComplete = false;
    public string[] choiceLines1;
    public string[] choiceLines2;

    //if an object you interact with has an event after the dialogue, it will be loaded into here for reference
    [Header("Event Values")]
    public GameObject eventObject;
    public GameObject choiceEvent;
    void Update()
    {
        //Debug.Log(eventObject);
        if (Input.GetKeyDown(KeyCode.E) && choicePause == false)
        {
            //if text is complete, go to next line
            if (textGUI.text == lines[index])
            {
                NextLine();
            }
            else
            {
                //if text wasnt complete but you pressed E, make it complete instantly
                StopAllCoroutines();
                textGUI.text = lines[index];
            }
        }
    }
    public void StartDialogue()
    {
        textGUI.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        //write each letter after a short pause
        foreach (char c in lines[index].ToCharArray())
        {
            textGUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        //if there are more lines left, count down 1 line and continue the text typing
        if (index < lines.Length - 1)
        {
            index++;
            textGUI.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        //if theres no more text left, check if there is multi choice
        else
        {
            //if there is no multi choice, end dialogue and let player move
            //and run any event if available from reference
            if (choiceLines1.Length == 0)
            {
                Invoke("EnablePlayer", 0.1f);
                textGUI.text = string.Empty;
                index = 0;
                if (eventObject != null)
                {
                    eventObject.transform.GetComponent<IEventRunner>().RunEvent();
                }
                if (choiceEvent != null && choiceComplete == true)
                {
                    choiceEvent.transform.GetComponent<IEventRunner>().RunEvent();
                }
            }
            //if there IS multi choice, give choice buttons and wait
            else
            {
                StopAllCoroutines();
                textGUI.text = lines[index];
                choicePause = true;
                choiceButton1.gameObject.SetActive(true);
                choiceButton2.gameObject.SetActive(true);
                choiceButton1.onClick.AddListener(LoadChoice1);
                choiceButton2.onClick.AddListener(LoadChoice2);

                //give choice
                //load next lines
                //reset dialogue runner to start next sequence after choice
            }
        }
    }
    public void EnablePlayer()
    {
        textBox.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        rayInt.enabled = true;
        move.enabled = true;
    }
    public void LoadChoice1()
    {
        //load the correct shit and continue normal dialogue procedures
        choicePause = false;
        choiceComplete = true;
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        lines = choiceLines1;
        StartDialogue();
        Invoke("ResetChoice", 0.1f);
    }
    public void LoadChoice2()
    {
        choicePause = false;
        choiceComplete = true;
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        lines = choiceLines2;
        StartDialogue();
        Invoke("ResetChoice", 0.1f);
    }
    void ResetChoice()
    {
        System.Array.Resize(ref choiceLines1, choiceLines1.Length - choiceLines1.Length);
    }
}
