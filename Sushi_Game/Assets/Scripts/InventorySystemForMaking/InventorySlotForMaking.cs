using System;
using UnityEngine;

public class InventorySlotForMaking : MonoBehaviour
{
    public InventoryItemForMaking inventoryItemForMaking;
    public InventoryManagerForMaking inventoryManagerForMaking;

    public void InitializeInventorySlotsForMaking(InventoryManagerForMaking inventoryManagerForMaking)
    {
        this.inventoryManagerForMaking = inventoryManagerForMaking;
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
    }
}