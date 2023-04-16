using UnityEngine;
using SFInventory;

public class OpenGate : MonoBehaviour, IInteractable
{
    public SFInventoryManager sFInventoryManager;
    public GameObject leftGate;
    public GameObject rightGate;
    private bool gateOpened = false;

    public void Interact()
    {
        if (!gateOpened)
        {
            bool keyPresent = false;
            foreach (SFInventoryCell cell in sFInventoryManager.inventoryCells)
            {
                if (cell.item != null && cell.item.name == "Key" && cell.itemsCount >= 1)
                {
                    keyPresent = true;
                    break;
                }
            }

            if (keyPresent)
            {
                leftGate.transform.Rotate(0, -60, 0);
                rightGate.transform.Rotate(0, 60, 0);
                gateOpened = true;
                Debug.Log("Key item is present in the inventory with at least 1 amount. The gate has been opened.");
            }
            else
            {
                Debug.Log("Key item is not present in the inventory or its amount is less than 1. The gate cannot be opened.");
            }
        }
        else
        {
            Debug.Log("The gate has already been opened and cannot be opened again.");
        }
    }
}

