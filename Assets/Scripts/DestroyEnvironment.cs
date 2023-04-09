using UnityEngine;
using SFInventory;

public class DestroyEnvironment : MonoBehaviour, IInteractable
{
    public SFInventoryManager sFInventoryManager;

    public void Interact()
    {
        bool bottlePresent = false;
        foreach (SFInventoryCell cell in sFInventoryManager.inventoryCells)
        {
            if (cell.item != null && cell.item.name == "Bottle" && cell.itemsCount >= 1)
            {
                bottlePresent = true;
                break;
            }
        }

        if (bottlePresent)
        {
            this.gameObject.SetActive(false);
            Debug.Log("Bottle item is present in the inventory with at least 1 amount.");
        }
        else
        {
            Debug.Log("Bottle item is not present in the inventory or its amount is less than 1.");
        }
    }
}
