using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.F))
        {
            inventoryUI.SetActive (!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        // check in the entire lenght of the array called slots of InventorySlot type
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                // take each slot and call AddItem from InventorySlot class
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
