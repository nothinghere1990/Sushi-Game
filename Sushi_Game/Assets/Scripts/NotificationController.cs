using System;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    private Animator close;
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    public MoneyController moneyController;
    public int stockingCost;

    public void Start()
    {
        transform.GetComponent<Button>().enabled = true;
        close = GetComponent<Animator>();
    }

    public void CloseNotification()
    {
        transform.GetComponent<Button>().enabled = false;
        close.SetTrigger("CloseNotification");
    }

    public void PickUpItem(int ingredientTypeCount)
    {
        moneyController.StockingSpendMoney(stockingCost);
        
        for (int i = 0; i < ingredientTypeCount; i++)
        {
            inventoryManager.FindSlot(itemsToPickup[i]);
        }
    }
}
