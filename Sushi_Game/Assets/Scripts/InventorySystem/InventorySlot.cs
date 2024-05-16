using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Image image;
    public MoneyController moneyController;

    public void Dropped()
    {
        for (int i = 0;
             i < inventoryManager.inventorySlots.Length;
             i++)
        {
            if (!inventoryManager.modeSwitch &&
                transform == inventoryManager.inventorySlots[i].transform)
            {
                for (int j = 0;
                     j < inventoryManager.inventorySlots.Length;
                     j++)
                {
                    if (inventoryManager.currentDraggingItem != null &&
                        inventoryManager.currentDraggingItem.parentAfterDrag ==
                        inventoryManager.inventorySlots[j].transform)
                    {
                        if (i < inventoryManager.inventorySlots.Length / 2 &&
                            j >= inventoryManager.inventorySlots.Length / 2)
                        {
                            moneyController.StockingEarnMoney(inventoryManager.currentDraggingItem.itemPrice *
                                                      inventoryManager.currentDraggingItem.itemCount);
                        }
                        else if (i >= inventoryManager.inventorySlots.Length / 2 &&
                                 j < inventoryManager.inventorySlots.Length / 2)
                        {
                            moneyController.StockingSpendMoney(inventoryManager.currentDraggingItem.itemPrice *
                                                       inventoryManager.currentDraggingItem.itemCount);
                        }
                    }
                }
            }
        }

        inventoryManager.clickedInventorySlot = transform;
        inventoryManager.ManagerDropped();
    }
}