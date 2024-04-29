using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerForMaking : MonoBehaviour
{
    public InventorySlotForMaking[] playerIngredientSlots;
    public GameObject inventoryItemPrefabForMaking;
    public Transform draggingItem;

    public void Start()
    {
        ManagerInitializesInventorySlotsForMaking();
        BringItemFromLastScene();
    }

    public void ManagerInitializesInventorySlotsForMaking()
    {
        for (int i = 0; i < playerIngredientSlots.Length; i++)
        {
            playerIngredientSlots[i]
                .InitializeInventorySlotsForMaking(transform.GetComponent<InventoryManagerForMaking>());
        }
    }

    public void BringItemFromLastScene()
    {
        for (int i = 0;
             i < InventoryManager.CarryPlayerIngredientImgs.Length;
             i++)
        {
            if (InventoryManager.CarryPlayerIngredientImgs[i] != null)
            {
                GameObject newItemGo = Instantiate(inventoryItemPrefabForMaking, playerIngredientSlots[i].transform);
                InventoryItemForMaking inventoryItemForMaking = newItemGo.GetComponent<InventoryItemForMaking>();
                inventoryItemForMaking.InitialiseItemFromLastScene(
                    InventoryManager.CarryPlayerIngredientImgs[i],
                    InventoryManager.CarryPlayerIngredientCounts[i],
                    transform.GetComponent<InventoryManagerForMaking>());
            }
        }
    }

    public void ClickOutsideOfSlots()
    {
        if (draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag.childCount == 0)
        {
            draggingItem.SetParent(draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag);
            draggingItem.GetComponent<Image>().raycastTarget = true;
            draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag = null;
            draggingItem = null;
        }
        else
        {
            draggingItem.GetComponent<InventoryItemForMaking>().parentAfterDrag
                .GetComponentInChildren<InventoryItemForMaking>().CheckIfHoldItemAndPutBackAndPickup();
        }
    }
}