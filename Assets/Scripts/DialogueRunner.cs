using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueRunner : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI textGUI;
    [SerializeField] private GameObject textBox;
    [SerializeField] private float textSpeed;
    [SerializeField] private int index;
    public string[] lines;

    //public LoadDialogue loadLines;

    [Header("Player Scripts")]
    [SerializeField] private RayInteraction rayInt;
    [SerializeField] private MovementAlt move;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textGUI.text == lines[index])
            {
                NextLine();
            }
            else
            {
                Debug.Log("Stop text ran!");
                StopAllCoroutines();
                textGUI.text = lines[index];
            }
        }
    }
    public void StartDialogue()
    {
        Debug.Log("Dialogue Start ran!");
        textGUI.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textGUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textGUI.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textBox.gameObject.SetActive(false);
            EnablePlayer();
            textGUI.text = string.Empty;
            index = 0;
        }
    }
    public void EnablePlayer()
    {
        rayInt.enabled = true;
        move.enabled = true;
    }
}
