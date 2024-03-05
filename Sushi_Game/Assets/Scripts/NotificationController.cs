using System;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    private Animator close;
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void Start()
    {
        close = GetComponent<Animator>();
    }

    public void CloseNotification()
    {
        close.SetTrigger("CloseNotification");
    }

    public void PickUpItem(int ingredientTypeCount)
    {
        for (int i = 0; i < ingredientTypeCount; i++)
        {
            inventoryManager.FindSlot(itemsToPickup[i]);
        }
    }
}
