using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFInventory;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Pop-up Settings")]
    [SerializeField] private Sprite[] images;
    [SerializeField] private string text;

    [Header("Item Settings")]
    [SerializeField] SFInventoryItem item;
    [SerializeField] SFInventoryManager inventory;
    public GameObject bottle;

    private bool activated = false;
    private int spriteState = 0;
    private Image activeImage;

    private MovementAlt move;
    private LightAlt lightAlt;
    private RayInteraction rayInt;
    private GameObject playerDialogue;
    private FadeOutText popupBox;
    void Start()
    {
        move = GameObject.Find("Player").GetComponent<MovementAlt>();
        rayInt = GameObject.Find("Player").GetComponent<RayInteraction>();
        lightAlt = move.gameObject.GetComponentInChildren<LightAlt>();
        playerDialogue = GameObject.Find("Dialogue");
        activeImage = playerDialogue.transform.Find("Image").GetComponent<Image>();
        popupBox = playerDialogue.transform.Find("PopUp").GetComponent<FadeOutText>();
    }
    public void Interact()
    {
        if (images.Length != 0)
        {

            if (activated == false)
            {
                rayInt.activeLocation = false;
                DisablePlayer();
                activated = true;
                activeImage.gameObject.SetActive(true);
                activeImage.sprite = images[0];
                spriteState++;

                popupBox.textDisplay.text = text;
                popupBox.textDisplay.color = new Color(popupBox.textDisplay.color.r, popupBox.textDisplay.color.g, popupBox.textDisplay.color.b, 1f);
                popupBox.TriggerFade();

                //inventory stuff

                Debug.Log("item taken");
                inventory.AddItem(item, 1);

            }
            else if (spriteState != images.Length)
            {
                activeImage.sprite = images[spriteState];
                activeImage.sprite = images[spriteState++];
            }
            else
            {

                activeImage.sprite = images[spriteState];

                rayInt.activeLocation = true;
                spriteState = 0;
                activated = false;
                activeImage.gameObject.SetActive(false);
                EnablePlayer();


                bottle.SetActive(false);

            }
        }
        else
        {
            popupBox.textDisplay.text = text;
            popupBox.textDisplay.color = new Color(popupBox.textDisplay.color.r, popupBox.textDisplay.color.g, popupBox.textDisplay.color.b, 1f);
            popupBox.TriggerFade();

            //inventory stuff

            Debug.Log("item taken");
            inventory.AddItem(item, 1);
            bottle.SetActive(false);

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
