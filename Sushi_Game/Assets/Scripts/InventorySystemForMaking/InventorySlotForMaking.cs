using System;
using UnityEngine;

public class InventorySlotForMaking : MonoBehaviour
{
    public int craftingSlotIndex;
    
    [HideInInspector] public InventoryManagerForMaking inventoryManagerForMaking;
    [HideInInspector] public InventoryItemForMaking inventoryItemForMaking;
    
    public void Start()
    {
        inventoryManagerForMaking = GameObject.Find("GameManager").GetComponent<InventoryManagerForMaking>();
    }

    public void ClickOnSlot()
    {
        if (transform.childCount == 0)
        {
            inventoryManagerForMaking.draggingItem.SetParent(transform);
            inventoryItemForMaking = transform.GetComponentInChildren<InventoryItemForMaking>();
            inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().image.raycastTarget = true;
            inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag = null;
            inventoryManagerForMaking.draggingItem = null;
        }
        else
        {
            inventoryItemForMaking = transform.GetComponentInChildren<InventoryItemForMaking>();
            inventoryItemForMaking.CheckIfHoldItemAndPutBackAndPickup();
        }

        foreach (var craftingSlot in inventoryManagerForMaking.craftingSlots)
        {
            if (transform == craftingSlot)
            {
                inventoryManagerForMaking.Crafting();
                return;
            }
        }
    }
}