using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour, IInteractable
{
    [SerializeField] private bool activated = false;
    [SerializeField] private int spriteState = 0;
    [SerializeField] private Image activeImage;
    [SerializeField] private Sprite[] images;
    [SerializeField] private GameObject playerDialogue;

    private MovementAlt move;
    private LightAlt lightAlt;
    private RayInteraction rayInt;
    void Start()
    {
        move = GameObject.Find("Player").GetComponent<MovementAlt>();
        rayInt = GameObject.Find("Player").GetComponent<RayInteraction>();
        lightAlt = move.gameObject.GetComponentInChildren<LightAlt>();
        playerDialogue = GameObject.Find("Dialogue");
        activeImage = playerDialogue.transform.Find("Image").GetComponent<Image>();
    }
    public void Interact()
    {
        if (activated == false)
        {
            rayInt.activeLocation = false;
            DisablePlayer();
            activated = true;
            activeImage.gameObject.SetActive(true);
            activeImage.sprite = images[0];
            spriteState++;
        }
        else if (spriteState != images.Length)
        {
            activeImage.sprite = images[spriteState];
            activeImage.sprite = images[spriteState++];
        }
        else
        {
            rayInt.activeLocation = true;
            spriteState = 0;
            activated = false;
            activeImage.gameObject.SetActive(false);
            activeImage.sprite = images[spriteState];
            EnablePlayer();
        }
    }
    private void DisablePlayer()
    {
        move.rb.velocity = Vector2.zero;
        move.moveDir.x = 0;
        move.moveDir.y = 0;
        move.enabled = false;
        //lightAlt.enabled = false;
    }
    private void EnablePlayer()
    {
        move.enabled = true;
        //lightAlt.enabled = false;
    }
}
