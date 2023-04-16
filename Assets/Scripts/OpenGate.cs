using UnityEngine;
using SFInventory;

public class OpenGate : MonoBehaviour
{
    public SFInventoryManager sFInventoryManager;
    public GameObject leftGate;
    public GameObject rightGate;

    public void Interact()
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
            Debug.Log("Key item is present in the inventory with at least 1 amount.");
        }
        else
        {
            Debug.Log("Key item is not present in the inventory or its amount is less than 1.");
        }
    }
}
