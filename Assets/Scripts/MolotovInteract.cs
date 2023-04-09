using UnityEngine;
using SFInventory;

public class MolotovInteract : MonoBehaviour
{
    [SerializeField] SFInventoryItem item;
    [SerializeField] SFInventoryManager inventory;
    public GameObject bottle;

    void OnCollisionEnter2D()
    {
        Debug.Log("boink");
        inventory.AddItem(item, 1);

        bottle.SetActive(false);
    }
}
