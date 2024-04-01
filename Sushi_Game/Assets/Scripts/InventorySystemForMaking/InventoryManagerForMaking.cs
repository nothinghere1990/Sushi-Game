using UnityEngine;

public class InventoryManagerForMaking : MonoBehaviour
{
    public InventorySlotForMaking[] playerIngredientSlots;
    public GameObject inventoryItemPrefabForMaking;

    public void Start()
    {
        BringItemFromLastScene();
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
                    InventoryManager.CarryPlayerIngredientCounts[i]);
            }
        }
    }

    public void FindPlayerIngredientSlots(Item item)
    {
        for (int i = 0;
             i < playerIngredientSlots.Length;
             i++)
        {
            InventorySlotForMaking slot = playerIngredientSlots[i];
            InventoryItemForMaking itemInSlot = slot.GetComponentInChildren<InventoryItemForMaking>();

            if (itemInSlot == null)
            {
                SpawnNewItemToPlayerIngredientSlots(item, slot);
                return;
            }
        }
    }

    public void SpawnNewItemToPlayerIngredientSlots(Item item, InventorySlotForMaking slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefabForMaking, slot.transform);
        InventoryItemForMaking inventoryItemForMaking = newItemGo.GetComponent<InventoryItemForMaking>();
        inventoryItemForMaking.InitialiseItem(item, transform.GetComponent<InventoryManagerForMaking>());
    }
}