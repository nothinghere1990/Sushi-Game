using System;
using UnityEngine;
using UnityEngine.UI;

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

        foreach (var servingSlot in inventoryManagerForMaking.servingTable)
        {
            if (transform == servingSlot)
            {
                foreach (var orderSlot in inventoryManagerForMaking.orderHanger)
                {
                    if (orderSlot.childCount != 0 &&
                        orderSlot.GetChild(0).GetComponent<InventoryItemForMaking>().sushiSpriteOnOrder ==
                        transform.GetChild(0).GetComponent<Image>().sprite)
                    {
                        inventoryManagerForMaking.moneyController.MakingEarnMoney(orderSlot.GetChild(0)
                            .GetComponent<InventoryItemForMaking>().orderPrice);
                        Destroy(orderSlot.GetChild(0).gameObject);
                        DestroyImmediate(transform.GetChild(0).gameObject);
                    }
                }
            }
        }
    }
}