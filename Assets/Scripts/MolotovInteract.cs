using UnityEngine;
using SFInventory;

public class MolotovInteract : MonoBehaviour, IInteractable
{
    [SerializeField] SFInventoryItem item;
    [SerializeField] SFInventoryManager inventory;
    public GameObject bottle;

    public void Interact()
    {
        Debug.Log("boink");
        inventory.AddItem(item, 1);

        bottle.SetActive(false);
    }
}
