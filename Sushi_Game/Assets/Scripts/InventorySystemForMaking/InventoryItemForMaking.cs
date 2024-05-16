using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemForMaking : MonoBehaviour
{
    public Transform parentAfterDrag;
    public int itemCount = 1;
    public Image image;
    public Sprite sushiSpriteOnOrder;
    public int orderPrice;

    [HideInInspector] public InventoryManagerForMaking inventoryManagerForMaking;
    [HideInInspector] public TMP_Text itemCountText;

    public void InitializeItemFromLastScene(Sprite imageFromLastScene, int itemCountFromLastScene,
        InventoryManagerForMaking inventoryManagerForMaking)
    {
        image.sprite = imageFromLastScene;
        itemCount = itemCountFromLastScene;
        itemCountText.text = itemCount.ToString();
        this.inventoryManagerForMaking = inventoryManagerForMaking;
    }

    public void InitializeItemFromCrafting(InventoryManagerForMaking inventoryManagerForMaking)
    {
        this.inventoryManagerForMaking = inventoryManagerForMaking;
        itemCountText.text = itemCount.ToString();
    }

    public void InitializeOrder(InventoryManagerForMaking inventoryManagerForMaking)
    {
        this.inventoryManagerForMaking = inventoryManagerForMaking;
        SushiMatching();
    }

    public void SushiMatching()
    {
        while (true)
        {
            foreach (var servingSlot in inventoryManagerForMaking.servingTable)
            {
                if (servingSlot.childCount != 0 &&
                    servingSlot.GetChild(0).GetComponent<Image>().sprite == sushiSpriteOnOrder)
                {
                    inventoryManagerForMaking.moneyController.MakingEarnMoney(orderPrice);
                }
            }
        }
    }

    public void CheckIfHoldItemAndPutBackAndPickup()
    {
        if (inventoryManagerForMaking.draggingItem == null)
        {
            DraggingAndSplitting();
        }
        else
        {
            if (inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                    .childCount == 0)
            {
                if (inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().image.sprite ==
                    image.sprite)
                {
                    itemCount += 1;
                    itemCountText.text = itemCount.ToString();
                    Destroy(inventoryManagerForMaking.draggingItem.gameObject);
                }
                else
                {
                    inventoryManagerForMaking.draggingItem.SetParent(inventoryManagerForMaking.draggingItem
                        .GetComponent<InventoryItemForMaking>().parentAfterDrag);
                    DraggingAndSplitting();
                }
            }
            else
            {
                if (inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().image.sprite ==
                    image.sprite)
                {
                    itemCount += 1;
                    itemCountText.text = itemCount.ToString();
                    Destroy(inventoryManagerForMaking.draggingItem.gameObject);
                }
                else
                {
                    inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                        .GetComponentInChildren<InventoryItemForMaking>().itemCount += 1;
                    inventoryManagerForMaking.draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                        .GetComponentInChildren<InventoryItemForMaking>().itemCountText.text = inventoryManagerForMaking
                        .draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                        .GetComponentInChildren<InventoryItemForMaking>().itemCount.ToString();
                    Destroy(inventoryManagerForMaking.draggingItem.gameObject);
                    DraggingAndSplitting();
                }
            }
        }
    }

    public void DraggingAndSplitting()
    {
        inventoryManagerForMaking.draggingItem = transform;
        parentAfterDrag = transform.parent;

        if (itemCount > 1)
        {
            GameObject newItemGo = Instantiate(gameObject, parentAfterDrag);
            InventoryItemForMaking inventoryItemForMakingAfterDrag = newItemGo.GetComponent<InventoryItemForMaking>();
            inventoryItemForMakingAfterDrag.itemCount = itemCount - 1;
            inventoryItemForMakingAfterDrag.itemCountText.text = inventoryItemForMakingAfterDrag.itemCount.ToString();
            inventoryItemForMakingAfterDrag.parentAfterDrag = null;
        }

        itemCount = 1;
        itemCountText.text = itemCount.ToString();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        ConnectToCrafting();
    }

    public void ConnectToCrafting()
    {
        foreach (var craftingSlot in inventoryManagerForMaking.craftingSlots)
        {
            if (parentAfterDrag == craftingSlot)
            {
                inventoryManagerForMaking.Crafting();
            }
        }

        if (parentAfterDrag == inventoryManagerForMaking.craftingResultSlot &&
            inventoryManagerForMaking.craftingResultSlot.childCount == 0)
        {
            inventoryManagerForMaking.RemoveItemFromCrafting();
        }
    }

    public void Update()
    {
        if (inventoryManagerForMaking.draggingItem != null && inventoryManagerForMaking.draggingItem == transform)
        {
            inventoryManagerForMaking.draggingItem.position = Input.mousePosition;
        }
    }
}