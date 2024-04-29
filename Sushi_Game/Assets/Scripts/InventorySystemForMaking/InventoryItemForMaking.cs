using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemForMaking : MonoBehaviour
{
    public Item item;
    public Transform parentAfterDrag;
    public int itemCount = 1;
    public Image image;

    [HideInInspector] public InventoryManagerForMaking inventoryManagerForMaking;
    [HideInInspector] public TMP_Text itemCountText;

    public void InitialiseItemFromLastScene(Sprite imageFromLastScene, int itemCountFromLastScene,
        InventoryManagerForMaking inventoryManagerForMaking)
    {
        image.sprite = imageFromLastScene;
        itemCount = itemCountFromLastScene;
        itemCountText.text = itemCount.ToString();
        this.inventoryManagerForMaking = inventoryManagerForMaking;
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
    }

    public void Update()
    {
        if (inventoryManagerForMaking.draggingItem == transform)
        {
            inventoryManagerForMaking.draggingItem.position = Input.mousePosition;
        }
    }
}